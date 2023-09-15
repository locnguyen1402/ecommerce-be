namespace ECommerce.Shared.Common.Constants;
public class JsonConstant
{
    private static JsonSerializerOptions? _jsonSerializerOptions;
    public static JsonSerializerOptions JsonSerializerOptions
    {
        get
        {
            if (_jsonSerializerOptions == null)
            {
                _jsonSerializerOptions = new JsonSerializerOptions
                {
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                };

                _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            }

            return _jsonSerializerOptions;
        }
    }

}