using IntegrifyLibrary.Domain;

namespace IntegrifyLibrary.Business;

public class CreateAuthorDto
{
    public List<Guid> BookIds { get; set; }
    public string AuthorName { get; set; } = string.Empty;
}

public class GetAuthorDto
{
    public Guid Id { get; init; }
    public List<Book>? Books { get; set; }
    public string AuthorName { get; set; } = string.Empty;
}

public class UpdateAuthorDto
{
    public List<Book>? Books { get; set; }
    public string AuthorName { get; set; } = string.Empty;
}