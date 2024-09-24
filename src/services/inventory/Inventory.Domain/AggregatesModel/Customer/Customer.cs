using ECommerce.Shared.Common.AggregatesModel.Auditing;
using ECommerce.Shared.Common.Enums;

namespace ECommerce.Inventory.Domain.AggregatesModel;

public class Customer(
    string firstName
    , string? lastName
    , string? userName
    , DateOnly? birthDate
    , Gender? gender
    , string? email
    , string? phoneNumber
) : AuditedAggregateRoot
{
    public string? UserName { get; private set; } = userName;
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
        string firstName
        , string? lastName
        , string? userName
        , DateOnly? birthDate
        , Gender? gender
        , string? email
        , string? phoneNumber
    )
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        FullName = string.Join(" ", new[] { firstName, lastName }.Where(x => !string.IsNullOrEmpty(x)));
        BirthDate = birthDate;
        Gender = gender;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public void UpdatePhoneNumber(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }

    public void SetRefUser(Guid refUserId)
    {
        RefUserId = refUserId;
    }
}