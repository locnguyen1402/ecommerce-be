using System.Linq.Expressions;

using ECommerce.Shared.Common.Queries;
using ECommerce.Shared.Common.Infrastructure.Specification;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Api.Vouchers.Specifications;

public class CheckExistedVoucherByCodeSpecification : Specification<Voucher>
{
    public CheckExistedVoucherByCodeSpecification
    (
        string code,
        PagingQuery? pagingQuery = null
    )
    {
        Builder.Where(x => x.Code.ToLower() == code.ToLower() && x.Status == VoucherStatus.IN_PROCESS);

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}

public class CheckExistedVoucherByCodeSpecification<TResult> : Specification<Voucher, TResult>
{
    public CheckExistedVoucherByCodeSpecification
    (
        Expression<Func<Voucher, TResult>> selector,
        string code,
        PagingQuery? pagingQuery = null
    ) : base(selector)
    {
        Builder.Where(x => x.Code.ToLower() == code.ToLower() && x.Status == VoucherStatus.IN_PROCESS);

        if (pagingQuery != null)
        {
            Builder.Paginate(pagingQuery);
        }
    }
}