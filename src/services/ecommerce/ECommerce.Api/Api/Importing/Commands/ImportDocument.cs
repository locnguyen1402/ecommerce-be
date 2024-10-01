using Microsoft.AspNetCore.Mvc;
using FluentValidation;

using ECommerce.Shared.Common.Constants;
using ECommerce.Shared.Common.Infrastructure.Endpoint;
using ECommerce.Shared.Common.Infrastructure.Services;
using ECommerce.Shared.Common.Extensions;
using ECommerce.Shared.Common.Enums;

using ECommerce.Api.Importing.Requests;
using ECommerce.Api.ObjectStorages.Requests;
using ECommerce.Api.Services;
using ECommerce.Domain.AggregatesModel;

namespace ECommerce.Api.Importing.Commands;

public class ImportDocumentCommandHandler : IEndpointHandler
{
    public Delegate Handle
    => async (
        [FromForm] ImportDocumentRequest request,
        IValidator<ImportDocumentRequest> validator,
        IImportHistoryRepository importHistoryRepository,
        IIdentityService identityService,
        IObjectService objectService,
        ILogger<ImportDocumentCommandHandler> logger,
        CancellationToken cancellationToken
    ) =>
    {
        logger.LogInformation("Start to import document file.");

        var invalidForm = await validator.ValidateAsync(request, cancellationToken);
        if (!invalidForm.IsValid)
            return Results.BadRequest(invalidForm.Errors);

        string name = request.File.GetFileName();
        string extension = request.File.GetFileExtension();
        string contentType = request.File.ContentType;
        long size = request.File.Length;

        var importDocument = new Document()
        {
            Name = name,
            Extension = extension,
            ContentType = contentType,
            Size = size,
        };

        ImportHistory importHistory = new(request.DocumentType, importDocument);
        importHistory.SetNotes(request.Notes);

        var userId = identityService.UserId;

        using var fileStream = new MemoryStream();
        await request.File.CopyToAsync(fileStream);
        fileStream.Seek(0, SeekOrigin.Begin);

        var uploadFileRequest = new UploadFileRequest()
        {
            Path = SetDocumentPath(request.DocumentType),
            File = request.File
        };

        var uploadResponse = await objectService.UploadFileAsync(uploadFileRequest, cancellationToken);

        if (uploadResponse == null)
        {
            importHistory.FailedToUpload("Failed to upload file to object storage.");

            await importHistoryRepository.AddAndSaveChangeAsync(importHistory, cancellationToken);

            return Results.BadRequest("Failed to upload file to object storage.");
        }

        importHistory.Uploaded(uploadResponse.Id, uploadResponse.Path, uploadResponse.Bucket);
        importHistory.AuditUpdate(userId);

        await importHistoryRepository.AddAndSaveChangeAsync(importHistory, cancellationToken);

        return TypedResults.NoContent();
    };

    private static string SetDocumentPath(ImportDocumentType documentType)
    {
        return documentType switch
        {
            ImportDocumentType.MASS_UPDATE_PRODUCT_BASE_INFO => DocumentPathConstant.PATH_IMPORTING_MASS_UPDATE_PRODUCT_BASE_INFO,
            ImportDocumentType.MASS_UPDATE_PRODUCT_SALES_INFO => DocumentPathConstant.PATH_IMPORTING_MASS_UPDATE_PRODUCT_SALES_INFO,
            _ => throw new ArgumentOutOfRangeException(nameof(documentType), documentType, null)
        };
    }
}