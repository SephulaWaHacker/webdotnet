using System.Text.Json;
using System.Text.Json.Serialization;
using DateOnlyTimeOnly.AspNet.Converters;

namespace BMW.CloudAdoption.BOM.Core.Helpers;

public static class SerializationHelpers
{
    public static string Serialize(this object data)
        => JsonSerializer.Serialize(data, new JsonSerializerOptions
        {
            Converters =
            {
                new DateOnlyJsonConverter(),
                new TimeOnlyJsonConverter(),
                new JsonStringEnumConverter()
            }
        });
    
    public static T Deserialize<T>(this string data)
        => JsonSerializer.Deserialize<T>(data, new JsonSerializerOptions
        {
            Converters =
            {
                new DateOnlyJsonConverter(),
                new TimeOnlyJsonConverter(),
                new JsonStringEnumConverter()
            }
        })!;
}