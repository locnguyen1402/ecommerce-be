using ECommerce.Shared.Common.AggregatesModel.Auditing;
using ECommerce.Shared.Common.Enums;
using ECommerce.Shared.Libs.Domain;
namespace ECommerce.Inventory.Domain.AggregatesModel;

public class ImportHistory : AuditedAggregateRoot
{
    public ImportDocumentType DocumentType { get; init; } = ImportDocumentType.UNSPECIFIED;
    public ImportStatus Status { get; private set; } = ImportStatus.UNSPECIFIED;

    public Document Document { get; private set; }

    public string? Remarks { get; private set; } = string.Empty;

    public string? Notes { get; private set; } = string.Empty;

    public List<LogDetail> Logs { get; private set; } = [];

    public List<ImportEvent> Events { get; private set; } = [];

    public ImportHistory(ImportDocumentType documentType, Document document)
    {
        Status = ImportStatus.UPLOADING;

        DocumentType = documentType;
        Document = document;
    }

    public void SetNotes(string? notes) => Notes = notes;

    public void Uploaded(Guid documentId, string path, string bucket)
    {
        Status = ImportStatus.UPLOADED;

        Document.SetDocumentInfo(documentId, path, bucket);
    }

    public void FailedToUpload(string remarks)
    {
        Status = ImportStatus.UPLOAD_FAILED;

        SetRemarks(remarks);
    }

    public void Processing(string remarks)
    {
        Status = ImportStatus.PROCESSING;

        SetRemarks(remarks);
    }

    public void Failed(string remarks)
    {
        Status = ImportStatus.FAILED;

        SetRemarks(remarks);
    }

    public void Succeeded(string remarks)
    {
        Status = ImportStatus.SUCCESS;

        SetRemarks(remarks);
    }

    public void PartialSuccess(string remarks)
    {
        Status = ImportStatus.PARTIAL_SUCCESS;

        SetRemarks(remarks);
    }

    public void SetLogDetail(LogDetail log)
    {
        Logs.Add(log);
    }

    public void Describe(string title, string description)
    {
        Document.Title = title;
        Document.Description = description;
    }

    public void AddOrUpdateTag(string key, string value)
    {
        if (Document.Tags.ContainsKey(key))
        {
            Document.Tags[key] = value;
        }
        else
        {
            Document.Tags.Add(key, value);
        }
    }

    public void AddEvent(ImportEvent importEvent)
    {
        Events.Add(importEvent);
    }

    public void SetRemarks(string remarks)
    {
        Remarks = remarks;
    }
}

public class Document : ValueObject
{
    public Guid? Id { get; set; }
    public string Bucket { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long Size { get; set; }

    public string? Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;

    public Dictionary<string, string> Tags { get; set; } = [];

    public string GetFilePath() => Id == null ? string.Empty : $"{Path}/{Id}{Extension}";

    public void SetDocumentInfo(
        Guid id,
        string path,
        string bucket
    )
    {
        Id = id;
        Path = path;
        Bucket = bucket;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id ?? Guid.Empty;
        yield return Bucket;
        yield return Path;
        yield return Name;
        yield return Extension;
        yield return ContentType;
        yield return Size;
        yield return Title ?? string.Empty;
        yield return Description ?? string.Empty;
        yield return Tags;
    }
}

public class LogDetail : ValueObject
{
    public string? RefId { get; set; } = string.Empty;
    public int RowNumber { get; set; }
    public IList<string> Errors { get; set; } = new List<string>();

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return RefId ?? string.Empty;
        yield return RowNumber;
        yield return Errors;
    }
}