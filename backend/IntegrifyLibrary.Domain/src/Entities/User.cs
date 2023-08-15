using System.Text.Json.Serialization;

namespace IntegrifyLibrary.Domain;

public class User
{
    public Guid UserId { get; init; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; init; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public DateOnly CreatedAt { get; init; }
    public DateOnly UpdatedAt { get; set; }


}
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Role
{
    Librarian,
    User
}

