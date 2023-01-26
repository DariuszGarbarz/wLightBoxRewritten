using log4net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using wLightBoxRewritten.Api.JsonConverters;

namespace wLightBoxRewritten.Api;

public interface IApiHttpClient
{
    Task<TResult> Send<TResult>(HttpMethod method, string uri, object? content);
}

public class ApiHttpClient : IApiHttpClient
{
    private const string ContentType = "application/json";

    private readonly HttpClient _httpClient;
    private readonly ILog _log;
    private readonly Encoding _encoding = Encoding.UTF8;
    private readonly JsonSerializerOptions _jsonOptions;

    public ApiHttpClient(
        HttpClient httpClient,
        ILog log)
    {
        _httpClient = httpClient;
        _log = log;
        _jsonOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };
        _jsonOptions.Converters.Add(new StringToEnumFactory());
    }

    public async Task<TResult> Send<TResult>(
        HttpMethod method,
        string uri,
        object? content)
    {
        var request = PrepareRequest(method, ref uri, content);

        _log.Debug($"-> {method} {uri}");

        var response = await _httpClient.SendAsync(request);
        _log.Debug($"<- {(int)response.StatusCode} {response.StatusCode} {uri}");

        var serializedResult = await response.Content.ReadAsStringAsync();
        _log.Debug($"<- {uri} Content: {serializedResult}");

        response.EnsureSuccessStatusCode();

        var deserializedResult = DeserializeResponse<TResult>(serializedResult)
            ?? throw new JsonException($"Could not deserialize {serializedResult} to {typeof(TResult)}");

        return deserializedResult;
    }

    private HttpRequestMessage PrepareRequest(
        HttpMethod method,
        ref string uri,
        object? content)
    {
        var request = new HttpRequestMessage(method, uri);

        if (content != null)
        {
            var serializedContent = SerializeBody(content);
            request.Content = new StringContent(serializedContent, _encoding, ContentType);
        }

        return request;
    }

    private string SerializeBody(object? content)
    {
        return JsonSerializer.Serialize(content, _jsonOptions);
    }

    private TResult? DeserializeResponse<TResult>(string jsonResult)
    {
        return JsonSerializer.Deserialize<TResult>(jsonResult, _jsonOptions);
    }
}

