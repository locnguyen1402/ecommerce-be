using ECommerce.Shared.Common.AggregatesModel.Auditing;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Domain.AggregatesModel;

public class Customer(
    string firstName
    , string? lastName
    , DateOnly? birthDate
    , Gender? gender
    , string? email
    , string? phoneNumber
) : AuditedAggregateRoot
{
    public string? UserName { get; private set; }
    public string FirstName { get; private set; } = firstName;
    public string? LastName { get; private set; } = lastName;
    public string FullName { get; private set; } = string.Join(" ", new[] { firstName, lastName }.Where(x => !string.IsNullOrEmpty(x)));
    public DateOnly? BirthDate { get; private set; } = birthDate;
    public Gender? Gender { get; private set; } = gender;
    public string? Email { get; private set; } = email;
    public string? PhoneNumber { get; private set; } = phoneNumber;
    public Guid? RefUserId { get; private set; }
    public CustomerLevelType LevelType { get; private set; } = CustomerLevelType.SILVER;
    private readonly List<Contact> _contacts = [];
    public virtual IReadOnlyCollection<Contact> Contacts => _contacts;
    public readonly List<Order> _orders = [];
    public virtual IReadOnlyCollection<Order> Orders => _orders;

    public void UpdateGeneralInfo(
        DateOnly? birthDate
        , Gender? gender
        , string? email
    )
    {
        BirthDate = birthDate;
        Gender = gender;
        Email = email;
    }
    public void UpdateName(string firstName, string? lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        FullName = string.Join(" ", new[] { firstName, lastName }.Where(x => !string.IsNullOrEmpty(x)));
    }

    public void UpdatePhoneNumber(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }

    public void SetUserName(string userName)
    {
        UserName = userName;
    }

    public void SetRefUser(Guid refUserId)
    {
        RefUserId = refUserId;
    }
}