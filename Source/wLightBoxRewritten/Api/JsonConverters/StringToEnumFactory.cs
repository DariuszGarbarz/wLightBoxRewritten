using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace wLightBoxRewritten.Api.JsonConverters;

public class StringToEnumFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsEnum;
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        JsonConverter converter = (JsonConverter)Activator.CreateInstance(
            typeof(StringToEnumConverter<>).MakeGenericType(typeToConvert),
            BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            args: new object[] { options },
            culture: null)!;

        return converter;
    }
}