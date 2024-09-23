namespace ECommerce.Inventory.Api.Identities.Connect.Requests;

public class ExchangeTokenRequest
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? GrantType { get; set; }
    public string? Scope { get; set; }
    public string? ClientId { get; set; }
}