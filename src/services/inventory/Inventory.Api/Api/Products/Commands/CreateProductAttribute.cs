using Microsoft.EntityFrameworkCore;

using MediatR;

using ECommerce.Shared.Common.AggregatesModel.Response;
using ECommerce.Inventory.Domain.AggregatesModel;

namespace ECommerce.Inventory.Api.Products.Commands;

public class CreateProductAttributeCommand : IRequest<IdResponse>
{
    private string _name = string.Empty;
    public string Name
    {
        get => _name.ToLower();
        set => _name = value;
    }
}

public class CreateProductAttributeCommandHandler(
    IProductAttributeRepository productAttributeRepository
) : IRequestHandler<CreateProductAttributeCommand, IdResponse>
{
    public async Task<IdResponse> Handle(CreateProductAttributeCommand request, CancellationToken cancellationToken)
    {
        var productAttribute = await productAttributeRepository.Query.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);

        if (productAttribute != null)
        {
            return new IdResponse(productAttribute.Id.ToString());
        }

        var newProductAttribute = new ProductAttribute(request.Name);

        productAttributeRepository.Add(newProductAttribute);
        await productAttributeRepository.SaveChangesAsync();

        return new IdResponse(newProductAttribute.Id.ToString());
    }
}