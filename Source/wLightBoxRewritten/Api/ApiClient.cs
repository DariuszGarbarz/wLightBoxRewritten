using log4net;
using System.Net.Http;
using static wLightBoxRewritten.Api.Endpoints;

namespace wLightBoxRewritten.Api;

public interface IApiClient
{
}

public class ApiClient : IApiClient
{
    private const int RetryCount = 3;

    private readonly IApiHttpClient _apiHttpClient;
    private readonly ILog _log;

    public ApiClient(IApiHttpClient apiHttpClient, ILog log)
    {
        _apiHttpClient = apiHttpClient;
        _log = log;
    }

    private async Task<TResult> Post<TResult>(
            string uri,
            object? content = null)
    {
        return await TrySend<TResult>(HttpMethod.Post, uri, content);
    }

    private async Task<TResult> Get<TResult>(
        string uri,
        object? content = null)
    {
        return await TrySend<TResult>(HttpMethod.Put, uri, content);
    }

    private async Task<TResult> TrySend<TResult>(
        HttpMethod method,
        string uri,
        object? content)
    {
        for (int count = 1; count <= RetryCount; count++)
        {
            try
            {
                var result = await _apiHttpClient.Send<TResult>(method, uri, content);

                return result;
            }
            catch (Exception ex) when (count < RetryCount)
            {
                await Task.Delay(TimeSpan.FromSeconds(3));

                _log.Warn($"Request to API failed. Resending request {count}", ex);
            }
        }

        throw new Exception($"Request to API {uri} failed");
    }
}
