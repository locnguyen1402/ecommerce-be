using FluentValidation;

using ECommerce.Shared.Common.Enums;

namespace ECommerce.Api.Importing.Requests;

/// <summary>
/// Request payload for importing master data.
/// </summary>
public record ImportDocumentRequest
{
    /// <summary>
    /// Type of importing data.
    /// </summary>
    public ImportDocumentType DocumentType { get; set; }

    /// <summary>
    /// File for importing data.
    /// </summary>
    public IFormFile File { get; set; } = null!;

    /// <summary>
    /// The notes of importing data.
    /// </summary>
    public string? Notes { get; set; }
}

/// <summary>
/// Validator for the ImportDocumentRequest class.
/// </summary>
public class ImportDocumentRequestValidator : AbstractValidator<ImportDocumentRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ImportDocumentRequestValidator"/> class.
    /// </summary>
    public ImportDocumentRequestValidator()
    {
        RuleFor(t => t.File)
            .NotNull();

        RuleFor(t => t.DocumentType)
            .IsInEnum()
            .NotEqual(ImportDocumentType.UNSPECIFIED);
    }
}