using ECommerce.Inventory.Api.Importing.Commands;
using ECommerce.Inventory.Api.Importing.Queries;
using ECommerce.Inventory.Api.Importing.Requests;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

namespace ECommerce.Inventory.Api.Endpoints;

public class ImportingEndpoint(WebApplication app) : MinimalEndpoint(app, "/inventory/importing")
{
    public override void Configure(RouteGroupBuilder builder)
    {
        builder.WithTags("Importing");
    }
    public override void MapEndpoints()
    {
        Builder.MapPost<ImportDocumentCommandHandler>("/")
            .Accepts<ImportDocumentRequest>("multipart/form-data")
            .DisableAntiforgery();

        Builder.MapGet<DownloadTemplateQuery>("/templates/{type}/download");

        Builder.MapGet<DownloadStreamQueryHandler>("/{id:guid}/download");
        Builder.MapGet<GetImportHistoriesQueryHandler>("/");
        Builder.MapGet<GetImportHistoryDetailQueryHandler>("/{id:guid}");
        Builder.MapGet<GetImportHistoryLogsByIdQueryHandler>("/{id:guid}/logs");
    }
}