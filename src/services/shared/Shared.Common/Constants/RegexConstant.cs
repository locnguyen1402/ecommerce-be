namespace ECommerce.Shared.Common.Constants;

public sealed partial class RegexConstants
{
    public const string CodePattern = @"^\d{6}$";
    public const string PhoneNumberPattern = @"^[0-9]{10}$";
    public const string UserNamePattern = @"^[a-zA-Z0-9@.]*$";
}
