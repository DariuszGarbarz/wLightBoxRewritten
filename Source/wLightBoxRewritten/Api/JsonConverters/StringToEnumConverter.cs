using System.Text.Json.Serialization;
using System.Text.Json;

namespace wLightBoxRewritten.Api.JsonConverters;

public class StringToEnumConverter<TValue> : JsonConverter<TValue> where TValue : struct
{
    public StringToEnumConverter(JsonSerializerOptions options)
    {
    }

    public override TValue Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) =>
            Enum.Parse<TValue>(reader.GetString() ?? throw new JsonException($"Could not convert to {typeof(TValue)}"));

    public override void Write(
        Utf8JsonWriter writer,
        TValue value,
        JsonSerializerOptions options) =>
            writer.WriteStringValue(Enum.Format(typeof(TValue), value, "D"));
}