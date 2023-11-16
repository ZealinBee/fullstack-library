using System.Text.Json.Serialization;

namespace IntegrifyLibrary.Domain;

public class User
{
    public Guid UserId { get; init; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public byte[] Salt { get; set; }
    public Role Role { get; set; }
    public DateOnly CreatedAt { get; init; }
    public DateOnly UpdatedAt { get; set; }
    public List<Loan> Loans { get; set; } = new();
    public string UserImage { get; set; } = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_640.png";
}
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Role
{
    User,
    Librarian
}

