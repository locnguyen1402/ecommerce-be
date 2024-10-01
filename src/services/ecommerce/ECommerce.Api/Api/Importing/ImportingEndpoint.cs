using ECommerce.Api.Importing.Commands;
using ECommerce.Api.Importing.Queries;
using ECommerce.Api.Importing.Requests;
using ECommerce.Shared.Common.Constants;
using ECommerce.Shared.Common.Infrastructure.Endpoint;

namespace ECommerce.Api.Endpoints;

public class ImportingEndpoint(WebApplication app) : MinimalEndpoint(app, $"/{EndpointConstants.PATH_SEGMENT}/importing")
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

        Builder.MapGet<DownloadStreamQueryHandler>("/{id:guid}/download");
        Builder.MapGet<GetImportHistoriesQueryHandler>("/");
        Builder.MapGet<GetImportHistoryDetailQueryHandler>("/{id:guid}");
        Builder.MapGet<GetImportHistoryLogsByIdQueryHandler>("/{id:guid}/logs");
    }
}