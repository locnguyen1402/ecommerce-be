namespace ECommerce.Shared.Common.DocumentProcessing;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false)]
public class ColumnAttribute : Attribute
{
    public ColumnAttribute() { }

    public ColumnAttribute(string columnName) : this()
    {
        ColumnName = columnName;
    }

    public ColumnAttribute(string columnName, DataType dataType, string format) : this(columnName)
    {
        DataType = dataType;
        Format = format;
    }

    public ColumnAttribute(Type resourceManagerProvider, string columnName) : this()
    {
        ColumnName = ResourceExtension.LookupResource(resourceManagerProvider, columnName);
    }

    public ColumnAttribute(Type resourceManagerProvider, string columnName, DataType dataType, string format) : this()
    {
        ColumnName = ResourceExtension.LookupResource(resourceManagerProvider, columnName);
        DataType = dataType;
        Format = format;
    }

    public string ColumnName { get; set; } = string.Empty;
    public DataType DataType { get; set; }
    public string? Format { get; set; }

    public bool Invisible { get; set; } = false;

    public bool ColumnGrouping { get; set; } = false;
    public string? ColumnGroupingNames { get; set; }
}
