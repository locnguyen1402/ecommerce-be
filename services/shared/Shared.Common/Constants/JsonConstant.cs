namespace ECommerce.Shared.Common.Constants;
public class JsonConstant
{
    public static JsonSerializerOptions JsonSerializerOptions
    {
        get
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
            };

            options.Converters.Add(new JsonStringEnumConverter());

            return options;
        }
    }
}