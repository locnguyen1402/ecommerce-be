using System.Linq.Expressions;

using ECommerce.Domain.AggregatesModel;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Api.Customers.Responses;

public record CustomerResponse(
    Guid Id,
    string? UserName,
    string FirstName,
    string? LastName,
    string FullName,
    DateOnly? BirthDate,
    Gender Gender,
    string? Email,
    string? PhoneNumber,
    CustomerLevelType LevelType
);

public record CustomerByUserIdsResponse(
    Guid Id,
    string FullName,
    string? UserName,
    Guid? UserId
);

public record CustomerByCustomerIdsResponse(
    Guid Id,
    string FullName,
    string? Email,
    string? PhoneNumber,
    DateOnly? BirthDate,
    Gender Gender
);

public static class CustomerProjection
{
    public static CustomerResponse ToCustomerResponse(this Customer customer)
    {
        return ToCustomerResponse().Compile().Invoke(customer);
    }

    public static List<CustomerResponse>? ToListCustomerResponse(this IEnumerable<Customer> customers)
    {
        return customers.Any() ? customers.Select(ToCustomerResponse().Compile()).ToList() : null;
    }

    public static Expression<Func<Customer, CustomerResponse>> ToCustomerResponse()
        => x =>
        new CustomerResponse(
            x.Id,
            x.UserName,
            x.FirstName,
            x.LastName,
            x.FullName,
            x.BirthDate,
            x.Gender ?? Gender.UNSPECIFIED,
            x.Email,
            x.PhoneNumber,
            x.LevelType
        );

    public static Expression<Func<Customer, CustomerByUserIdsResponse>> ToCustomerByUserIds()
        => x =>
        new CustomerByUserIdsResponse(
            x.Id,
            x.FullName,
            x.UserName,
            x.RefUserId
        );

    public static Expression<Func<Customer, CustomerByCustomerIdsResponse>> ToCustomerByCustomerIds()
        => x =>
        new CustomerByCustomerIdsResponse(
            x.Id,
            x.FullName,
            x.Email,
            x.PhoneNumber,
            x.BirthDate,
            x.Gender ?? Gender.UNSPECIFIED
        );
}