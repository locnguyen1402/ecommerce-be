namespace ECommerce.Catalog.Domain.AggregatesModel.AdministrativeUnits;

public class BaseUnit(string name, string? code)
{
    public string Name { get; private set; } = name;
    public string? Code { get; private set; } = code;
}