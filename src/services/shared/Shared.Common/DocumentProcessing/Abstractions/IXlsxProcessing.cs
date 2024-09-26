namespace ECommerce.Shared.Common.DocumentProcessing;

public interface IXlsxProcessing
{
    IEnumerable<TData> ExtractData<TData>(
        Stream fileStream,
        int sheetIndex = 1,
        int headerRowIndex = 1,
        int startRowIndex = 2) where TData : class, new();

    IEnumerable<TData> ExtractData1<TData>(
        Stream fileStream,
        int sheetIndex = 1,
        int headerRowIndex = 1,
        int startRowIndex = 2) where TData : class, new();

    MemoryStream ExportXlsxSteam<TItem>(IReadOnlyCollection<TItem> source, string sheetName);
    byte[] ExportXlsx<TItem>(IReadOnlyCollection<TItem> source, string sheetName);
    byte[] ExportGroupingXlsx<TItem, ISubItem>(IReadOnlyCollection<TItem> source, string sheetName);
}
