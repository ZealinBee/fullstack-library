using System.Text.Json.Serialization;

namespace IntegrifyLibrary.Domain;

public class User
{
    public Guid UserId { get; init; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public byte[] Salt { get; set; }
    public Role Role { get; set; }
    public DateOnly CreatedAt { get; init; }
    public DateOnly UpdatedAt { get; init; }
    public List<Loan> Loans { get; set; } = new();
}
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Role
{
    User,
    Librarian
}

