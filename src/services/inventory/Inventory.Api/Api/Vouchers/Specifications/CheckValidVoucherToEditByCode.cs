using System.Linq.Expressions;

using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Inventory.Domain.AggregatesModel;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Api.Vouchers.Specifications;

public class CheckValidVoucherToEditByCodeSpecification : Specification<Voucher>
{
    public CheckValidVoucherToEditByCodeSpecification
    (
        string code,
        PagingQuery? pagingQuery = null
    )
    {
        Builder.Where(x => x.Code.ToLower() == code.ToLower() && x.Status == VoucherStatus.NEW);

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}

public class CheckValidVoucherToEditByCodeSpecification<TResult> : Specification<Voucher, TResult>
{
    public CheckValidVoucherToEditByCodeSpecification
    (
        Expression<Func<Voucher, TResult>> selector,
        string code,
        PagingQuery? pagingQuery = null
    ) : base(selector)
    {
        Builder.Where(x => x.Code.ToLower() == code.ToLower() && x.Status == VoucherStatus.NEW);

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}