namespace ECommerce.Shared.Common.Constants;
public class JsonConstant
{
    public static JsonSerializerOptions JsonSerializerOptions
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