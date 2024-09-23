namespace ECommerce.Shared.Common.Infrastructure.Data;

public interface IExtraProperties { }

public interface IExtraProperties<TExtraProperties> : IExtraProperties
{
    TExtraProperties ExtraProperties { get; }
}
