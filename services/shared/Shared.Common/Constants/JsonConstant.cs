namespace ECommerce.Shared.Common.Constants;
public class JsonConstant
{
    public static JsonSerializerOptions jsonSerializerOptions
    {
        get
        {
            return new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
            };
        }
    }
}