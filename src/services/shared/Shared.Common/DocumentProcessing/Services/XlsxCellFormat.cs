namespace ECommerce.Shared.Common.DocumentProcessing;

public class XlsxCellFormat
{
    public string? Format { get; set; }
    public Type Type { get; set; } = typeof(string);
}