namespace ECommerce.Shared.Common.Validators;
public static class GuidValidator
{
    public static IRuleBuilderOptions<T, Guid> IsValidGuid<T>(this IRuleBuilder<T, Guid> builder)
    {
        return builder
            .Must(x => x != Guid.Empty)
            .Must(x =>
            {
                return Guid.TryParse(x.ToString(), out _);
            })
            .WithMessage("Invalid Guid value");
    }
}