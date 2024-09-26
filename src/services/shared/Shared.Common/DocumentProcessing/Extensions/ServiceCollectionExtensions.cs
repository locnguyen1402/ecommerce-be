using ClosedXML.Excel;
using ClosedXML.Graphics;

using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Shared.Common.DocumentProcessing;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDocumentProcessing(this IServiceCollection services)
    {
        LoadOptions.DefaultGraphicEngine = new DefaultGraphicEngine("Arial");

        services.AddSingleton<IXlsxProcessing, XlsxProcessing>();

        return services;
    }
}
