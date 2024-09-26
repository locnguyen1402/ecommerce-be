using System.Reflection;
using System.Text.RegularExpressions;

using ClosedXML.Excel;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

using Microsoft.Extensions.Logging;

namespace ECommerce.Shared.Common.DocumentProcessing;

public partial class XlsxProcessing : IXlsxProcessing
{
    private readonly ILogger<XlsxProcessing> _logger = null!;

    public XlsxProcessing(ILogger<XlsxProcessing> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IEnumerable<TData> ExtractData<TData>(
        Stream fileStream,
        int sheetIndex = 1,
        int headerRowIndex = 1,
        int startRowIndex = 2) where TData : class, new()
    {
        _logger.LogDebug("Extracting data from Excel file");
        long memory = GC.GetTotalMemory(true);
        _logger.LogDebug($"Memory before: {memory}");

        //Read the first Sheet from Excel file.
        using (XLWorkbook workbook = new(fileStream))
        {
            IXLWorksheet workSheet = workbook.Worksheet(sheetIndex);

            IXLRow headerRow = workSheet.Row(headerRowIndex);

            Dictionary<string, string> columnDefs = new();

            foreach (IXLCell cell in headerRow.CellsUsed())
            {
                columnDefs.Add(cell.Address.ColumnLetter, cell.GetString());
            }

            _logger.LogDebug("Column definitions: {@ColumnDefs}", columnDefs);

            // create dictionary of attribute DisplayName value and propertyInfo
            var properties = typeof(TData).GetProperties()
                .Select(p => new
                {
                    Name = p.GetCustomAttribute<ColumnAttribute>()?.ColumnName ?? string.Empty,
                    PropertyInfo = p
                });

            foreach (IXLRow row in workSheet.RowsUsed(x => x.RowNumber() >= startRowIndex))
            {
                TData rowData = new();

                foreach (IXLCell cell in row.CellsUsed(x => !x.Value.IsBlank))
                {
                    string cellValue = string.Empty;

                    if (cell.DataType == XLDataType.DateTime)
                    {
                        cellValue = cell.GetDateTime().ToString();
                    }
                    else if (DateTime.TryParse(cell.Value.ToString(), out DateTime date))
                    {
                        cellValue = date.ToString();
                    }
                    else
                    {
                        cellValue = cell.GetString().Trim();
                    }

                    string cellName = columnDefs[cell.Address.ColumnLetter];

                    _logger.LogDebug("Cell: {@Cell}", new { cellName, cellValue });

                    var propertyItem = properties.FirstOrDefault(p => p.Name.Equals(cellName, StringComparison.InvariantCultureIgnoreCase));

                    if (propertyItem == null || propertyItem.PropertyInfo == null)
                        continue;

                    var propertyInfo = propertyItem.PropertyInfo;
                    propertyInfo.SetValue(rowData, null);

                    if (propertyInfo.PropertyType == typeof(int) || propertyInfo.PropertyType.BaseType == typeof(Enum))
                    {
                        if (int.TryParse(cellValue, out int value))
                        {
                            propertyInfo.SetValue(rowData, value);
                        }
                    }
                    else if (propertyInfo.PropertyType == typeof(decimal))
                    {
                        if (decimal.TryParse(cellValue, out decimal value))
                        {
                            propertyInfo.SetValue(rowData, value);
                        }
                    }
                    else if (propertyInfo.PropertyType == typeof(bool))
                    {
                        var normalizedCellVale = cellValue.ToUpper();
                        var validTrueValues = new[] { "Y", "YES", "TRUE" };
                        var validFalseValues = new[] { "N", "NO", "FALSE" };

                        if (validTrueValues.Contains(normalizedCellVale))
                        {
                            propertyInfo.SetValue(rowData, true);
                        }
                        else if (validFalseValues.Contains(normalizedCellVale))
                        {
                            propertyInfo.SetValue(rowData, false);
                        }
                        else if (bool.TryParse(cellValue, out bool value))
                        {
                            propertyInfo.SetValue(rowData, value);
                        }
                    }
                    else
                    {
                        propertyInfo.SetValue(rowData, cellValue);
                    }
                }

                _logger.LogDebug("Row: {@Row}", rowData);

                yield return rowData;
            }
        }

        memory = GC.GetTotalMemory(true);
        _logger.LogDebug($"Memory after: {memory}");

        _logger.LogDebug("Data extraction completed");
    }

    public IEnumerable<TData> ExtractData1<TData>(
        Stream fileStream,
        int sheetIndex = 1,
        int headerRowIndex = 1,
        int startRowIndex = 2) where TData : class, new()
    {
        _logger.LogDebug("Extracting data from Excel file");
        long memory = GC.GetTotalMemory(true);
        _logger.LogDebug($"Memory before: {memory}");

        //Read the first Sheet from Excel file.
        using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(fileStream, false))
        {
            WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart!;
            WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
            SharedStringTablePart stringTablePart = workbookPart.SharedStringTablePart!;
            using OpenXmlReader reader = OpenXmlReader.Create(worksheetPart);
            Dictionary<string, string> columnDefs = new();
            var properties = typeof(TData).GetProperties()
            .Select(p => new
            {
                Name = p.GetCustomAttribute<ColumnAttribute>()?.ColumnName ?? string.Empty,
                PropertyInfo = p
            });

            while (reader.Read())
            {
                if (reader.ElementType == typeof(Row))
                {
                    Row row = (Row)reader.LoadCurrentElement()!;

                    if (row.RowIndex! == (uint)headerRowIndex)
                    {
                        var fieldNames = properties.Select(x => x.Name);

                        // Get list of header columns based on field names
                        foreach (Cell cell in row.Elements<Cell>())
                        {
                            if (cell.CellReference?.HasValue == false)
                                continue;

                            if (cell.CellValue == null)
                                continue;

                            string cellVale = stringTablePart.SharedStringTable!.ChildElements[Int32.Parse(cell.CellValue.InnerText)].InnerText;

                            if (fieldNames.Contains(cellVale, StringComparer.InvariantCultureIgnoreCase))
                                columnDefs.Add(GetColumnLetter(cell.CellReference!.Value!), cellVale);

                            if (columnDefs.Count == fieldNames.Count())
                                break;

                            continue;
                        }

                        _logger.LogDebug("Column definitions: {@ColumnDefs}", columnDefs);

                        continue;
                    }

                    TData rowData = new();

                    foreach (Cell cell in row.Elements<Cell>())
                    {
                        if (cell.CellReference?.HasValue == false)
                            continue;

                        if (!columnDefs.TryGetValue(GetColumnLetter(cell.CellReference!.Value!), out var cellName))
                            break;

                        if (string.IsNullOrEmpty(cellName))
                            continue;

                        if (cell.CellValue == null)
                            continue;

                        string cellVale = stringTablePart.SharedStringTable!.ChildElements[Int32.Parse(cell.CellValue.InnerText)].InnerText;

                        var propertyItem = properties.FirstOrDefault(p => p.Name.Equals(cellName, StringComparison.InvariantCultureIgnoreCase));

                        if (propertyItem == null || propertyItem.PropertyInfo == null)
                            continue;

                        var propertyInfo = propertyItem.PropertyInfo;
                        propertyInfo.SetValue(rowData, null);

                        if (propertyInfo.PropertyType == typeof(int) || propertyInfo.PropertyType.BaseType == typeof(Enum))
                        {
                            if (int.TryParse(cellVale, out int value))
                            {
                                propertyInfo.SetValue(rowData, value);
                            }
                        }
                        else if (propertyInfo.PropertyType == typeof(decimal))
                        {
                            if (decimal.TryParse(cellVale, out decimal value))
                            {
                                propertyInfo.SetValue(rowData, value);
                            }
                        }
                        else if (propertyInfo.PropertyType == typeof(bool))
                        {
                            var normalizedCellVale = cellVale.ToUpper();
                            var validTrueValues = new[] { "Y", "YES", "TRUE" };
                            var validFalseValues = new[] { "N", "NO", "FALSE" };

                            if (validTrueValues.Contains(normalizedCellVale))
                            {
                                propertyInfo.SetValue(rowData, true);
                            }
                            else if (validFalseValues.Contains(normalizedCellVale))
                            {
                                propertyInfo.SetValue(rowData, false);
                            }
                            else if (bool.TryParse(cellVale, out bool value))
                            {
                                propertyInfo.SetValue(rowData, value);
                            }
                        }
                        else
                        {
                            propertyInfo.SetValue(rowData, cellVale);
                        }
                    }

                    _logger.LogDebug("Row: {@Row}", rowData);

                    yield return rowData;
                }
            }
        }

        memory = GC.GetTotalMemory(true);
        _logger.LogDebug($"Memory after: {memory}");

        _logger.LogDebug("Data extraction completed");
    }

    public MemoryStream ExportXlsxSteam<TItem>(IReadOnlyCollection<TItem> source, string sheetName)
    {
        var header = new List<string>();

        foreach (PropertyInfo propertyInfo in typeof(TItem).GetProperties())
        {
            var attr = propertyInfo.GetCustomAttribute<ColumnAttribute>();
            if (attr?.Invisible == true) continue;

            header.Add(attr?.ColumnName ?? propertyInfo.Name);
        }

        using XLWorkbook workbook = new();
        IXLWorksheet worksheet = workbook.Worksheets.Add(sheetName);
        worksheet.Style.Font.FontName = "Arial";

        var headerRange = worksheet.Cell(1, 1).InsertData(new List<string[]> { header.ToArray() });

        headerRange.AddToNamed(nameof(headerRange));
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Hair;
        // headerRange.Style.Font

        foreach (var row in source)
        {
            GenerateRow(worksheet, row);

            // var rowData = new List<string>();

            // foreach (var propertyInfo in typeof(TItem).GetProperties())
            // {
            //     var attr = propertyInfo.GetCustomAttribute<ColumnAttribute>();
            //     if (attr?.Invisible == true) continue;

            //     var format = attr?.Format ?? string.Empty;
            //     var value = propertyInfo.GetValue(row);

            //     if (value is DateTime dateTimeValue)
            //     {
            //         value = dateTimeValue.ToString(format);
            //     }
            //     else if (value is DateTimeOffset dateTimeOffsetValue)
            //     {
            //         value = dateTimeOffsetValue.ToString(format);
            //     }
            //     else if (value is Decimal decimalValue)
            //     {
            //         value = decimalValue.ToString(format);
            //     }

            //     rowData.Add(value?.ToString() ?? string.Empty);
            // }

            // var cell = worksheet.Cell(worksheet.LastRowUsed()!.RowNumber() + 1, 1);
            // var rowRange = cell.InsertData(new List<string[]> { rowData.ToArray() });

            // rowRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            // rowRange.Style.Border.InsideBorder = XLBorderStyleValues.Hair;
        }

        // foreach (var column in dateTimeColumns)
        // {
        //     worksheet.Column(column).Style.NumberFormat.Format = "dd/MM/yyyy";
        // }

        // var dataRange = worksheet.Cell(2, 1).InsertData(source);

        // dataRange.AddToNamed(nameof(dataRange));

        // dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        // dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Hair;
        worksheet.Columns().AdjustToContents();

        MemoryStream stream = new();
        workbook.SaveAs(stream);

        stream.Seek(0, SeekOrigin.Begin);

        return stream;
    }

    private void GenerateRow<TItem>(IXLWorksheet worksheet, TItem? row)
    {
        bool hasChildren = false;
        List<TItem> children = [];

        var rowNumber = worksheet.LastRowUsed()!.RowNumber() + 1;
        int columnIndex = 1;

        foreach (var propertyInfo in typeof(TItem).GetProperties())
        {
            var attr = propertyInfo.GetCustomAttribute<ColumnAttribute>();

            var format = attr?.Format ?? string.Empty;
            var value = propertyInfo.GetValue(row);
            XlsxCellFormat cellFormat = new();

            if (value is DateTime dateTimeValue)
            {
                cellFormat.Format = format;
                cellFormat.Type = typeof(DateTime);
            }
            else if (value is DateTimeOffset dateTimeOffsetValue)
            {
                cellFormat.Format = format;
                cellFormat.Type = typeof(DateTime);
                value = dateTimeOffsetValue.DateTime;
            }
            else if (value is Decimal decimalValue)
            {
                cellFormat.Format = format;
                cellFormat.Type = typeof(decimal);
            }
            else if (value is IList<TItem> list && list.Count > 0)
            {
                hasChildren = true;
                children = [.. list];
            }

            if (attr?.Invisible == true) continue;

            var cell = worksheet.Cell(rowNumber, columnIndex);
            columnIndex++;

            cell.Value = XLCellValue.FromObject(value);
            if (cellFormat.Type == typeof(DateTime))
                cell.Style.DateFormat.Format = cellFormat.Format;
            else if (cellFormat.Type == typeof(decimal))
                cell.Style.NumberFormat.Format = cellFormat.Format;

            cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            cell.Style.Border.InsideBorder = XLBorderStyleValues.Hair;
        }

        if (hasChildren)
        {
            worksheet.Outline.SummaryVLocation = XLOutlineSummaryVLocation.Bottom;
            IXLRange rowRange = worksheet.Range(rowNumber, 1, rowNumber, columnIndex);
            rowRange.Style.Font.Bold = true;
            foreach (var item in children)
            {
                GenerateRow(worksheet, item);
            }
            worksheet.Rows(rowNumber + 1, rowNumber + children.Count).Group(true);
        }
    }

    public byte[] ExportXlsx<TItem>(IReadOnlyCollection<TItem> source, string sheetName)
    {
        var stream = ExportXlsxSteam(source, sheetName);

        return stream.ToArray();
    }

    public MemoryStream ExportGroupingXlsxSteam<TItem, TSubItem>(IReadOnlyCollection<TItem> source, string sheetName)
    {
        var header = new List<string>();

        foreach (PropertyInfo propertyInfo in typeof(TItem).GetProperties())
        {
            var attr = propertyInfo.GetCustomAttribute<ColumnAttribute>();
            if (attr?.Invisible == true) continue;

            if (attr?.ColumnGrouping == true)
            {
                var headerNames = attr.ColumnGroupingNames?.Split(',') ?? [];
                foreach (var name in headerNames)
                {
                    header.Add(name);
                }
            }
            else header.Add(attr?.ColumnName ?? propertyInfo.Name);
        }

        using XLWorkbook workbook = new();
        IXLWorksheet worksheet = workbook.Worksheets.Add(sheetName);
        worksheet.Style.Font.FontName = "Arial";

        var headerRange = worksheet.Cell(1, 1).InsertData(new List<string[]> { header.ToArray() });

        headerRange.AddToNamed(nameof(headerRange));
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Hair;
        // headerRange.Style.Font

        foreach (var row in source)
        {
            GenerateGroupingRow<TItem, TSubItem>(worksheet, row);
        }
        worksheet.Columns().AdjustToContents();

        MemoryStream stream = new();
        workbook.SaveAs(stream);

        stream.Seek(0, SeekOrigin.Begin);

        return stream;
    }

    public byte[] ExportGroupingXlsx<TItem, TSubItem>(IReadOnlyCollection<TItem> source, string sheetName)
    {
        var stream = ExportGroupingXlsxSteam<TItem, TSubItem>(source, sheetName);

        return stream.ToArray();
    }

    private void GenerateGroupingRow<TItem, TSubItem>(IXLWorksheet worksheet, TItem? row)
    {
        bool hasChildren = false;
        List<TItem> children = [];

        var rowData = new List<string>();
        foreach (var propertyInfo in typeof(TItem).GetProperties())
        {
            var attr = propertyInfo.GetCustomAttribute<ColumnAttribute>();

            var format = attr?.Format ?? string.Empty;
            var value = GetObjectValue(propertyInfo, row);

            if (value is IList<TItem> list && list.Count > 0)
            {
                hasChildren = true;
                children = [.. list];
            }

            if (attr?.Invisible == true) continue;

            if (attr?.ColumnGrouping == true)
            {
                IList<TSubItem> listSub = (IList<TSubItem>)value!;
                foreach (var subItem in listSub)
                {
                    foreach (var propertySubInfo in typeof(TSubItem).GetProperties())
                    {
                        var attrSub = propertySubInfo.GetCustomAttribute<ColumnAttribute>();
                        if (attrSub?.Invisible == true) continue;
                        var subValue = GetObjectValue(propertySubInfo, subItem);
                        rowData.Add(subValue?.ToString() ?? string.Empty);
                    }
                }
            }
            else rowData.Add(value?.ToString() ?? string.Empty);
        }

        var rowNumber = worksheet.LastRowUsed()!.RowNumber() + 1;
        var cell = worksheet.Cell(rowNumber, 1);
        var rowRange = cell.InsertData(new List<string[]> { rowData.ToArray() });

        rowRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        rowRange.Style.Border.InsideBorder = XLBorderStyleValues.Hair;

        if (hasChildren)
        {
            worksheet.Outline.SummaryVLocation = XLOutlineSummaryVLocation.Bottom;
            rowRange.Style.Font.Bold = true;
            foreach (var item in children)
            {
                GenerateRow(worksheet, item);
            }
            worksheet.Rows(rowNumber + 1, rowNumber + children.Count).Group(true);
        }
    }

    private object? GetObjectValue<T>(PropertyInfo propertyInfo, T row)
    {
        var value = propertyInfo.GetValue(row);

        var attr = propertyInfo.GetCustomAttribute<ColumnAttribute>();

        var format = attr?.Format ?? string.Empty;

        if (value is DateTime dateTimeValue)
        {
            value = dateTimeValue.ToString(format);
        }
        else if (value is DateTimeOffset dateTimeOffsetValue)
        {
            value = dateTimeOffsetValue.ToString(format);
        }
        else if (value is Decimal decimalValue)
        {
            value = decimalValue.ToString(format);
        }

        return value;
    }

    private static string GetColumnLetter(string cellReference)
    {
        Regex regex = MyRegex();
        Match match = regex.Match(cellReference);
        return match.Value;
    }

    [GeneratedRegex("[A-Za-z]+")]
    private static partial Regex MyRegex();
}



