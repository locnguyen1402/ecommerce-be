namespace ECommerce.Shared.Common.DocumentProcessing;

public class CellData
{
    public dynamic CellValue { get; set; } = string.Empty;
    public string Format { get; set; } = string.Empty;
    public DataType DataType { get; set; } = DataType.STRING;
}
