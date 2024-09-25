// using ECommerce.Shared.Common.Enums;
// using ECommerce.Shared.Common.Infrastructure.Endpoint;

// namespace ECommerce.Inventory.Api.Importing.Queries;

// /// <summary>
// /// inheritdoc
// /// </summary>
// public class DownloadTemplateQuery : IEndpointHandler
// {
//     /// <summary>
//     /// inheritdoc
//     /// </summary>
//     public Delegate Handler
//     => async (
//         ImportDocumentType type,
//         //IXlsxProcessing xlsxProcessing,
//         HttpContext httpContext,
//         CancellationToken cancellationToken
//     ) =>
//     {
//         httpContext.SetContentDispositionResponseHeader();

//         if (type == ImportDocumentType.AGREEMENTS)
//         {
//             var fileStream = xlsxProcessing.ExportXlsxSteam(new List<ImportAgreementsTemplate>(), "Template");

//             return TypedResults.Stream(fileStream, "application/octet-stream", "AgreementTemplate.xlsx");
//         }

//         if (type == ImportDocumentType.PAYMENT_HISTORIES)
//         {
//             var fileStream = xlsxProcessing.ExportXlsxSteam(new List<ImportPaymentHistoryTemplate>(), "Template");

//             return TypedResults.Stream(fileStream, "application/octet-stream", "PaymentHistoryTemplate.xlsx");
//         }

//         if (type == ImportDocumentType.ASSIGN_AGREEMENTS_EMPLOYEES || type == ImportDocumentType.UNASSIGN_AGREEMENTS_EMPLOYEES)
//         {
//             var fileStream = xlsxProcessing.ExportXlsxSteam(new List<ImportAgreementEmployeeTemplate>(), "Template");

//             return TypedResults.Stream(fileStream, "application/octet-stream", type == ImportDocumentType.ASSIGN_AGREEMENTS_EMPLOYEES ? "AssignAgreementsEmployeesTemplate.xlsx" : "UnassignAgreementsEmployeesTemplate.xlsx");
//         }

//         if (type == ImportDocumentType.ASSIGN_CAMPAIGN_AGREEMENTS || type == ImportDocumentType.UNASSIGN_CAMPAIGN_AGREEMENTS)
//         {
//             var fileStream = xlsxProcessing.ExportXlsxSteam(new List<ImportCampaignAgreementTemplate>(), "Template");

//             return TypedResults.Stream(fileStream, "application/octet-stream", type == ImportDocumentType.ASSIGN_CAMPAIGN_AGREEMENTS ? "AssignCampaignAgreementsTemplate.xlsx" : "UnassignCampaignAgreementsTemplate.xlsx");
//         }

//         if (type == ImportDocumentType.EMPLOYEES)
//         {
//             var fileStream = xlsxProcessing.ExportXlsxSteam(new List<ImportEmployeesTemplate>(), "Template");

//             return TypedResults.Stream(fileStream, "application/octet-stream", "EmployeeTemplate.xlsx");
//         }

//         return Results.BadRequest(new { title = "Invalid Import Template Type" });
//     };

// }

