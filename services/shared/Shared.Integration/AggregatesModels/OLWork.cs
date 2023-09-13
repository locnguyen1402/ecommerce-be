namespace ECommerce.Shared.Integration.AggregatesModels;
public class OLWork : OLEntity
{
}

public class OLWorkMapperProfile : Profile
{
    public OLWorkMapperProfile()
    {
        CreateMap<OLWork, Work>()
            .IncludeBase<OLEntity, BaseEntity>();
    }
}