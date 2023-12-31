using IntegrifyLibrary.Domain;

namespace IntegrifyLibrary.Business;

public class CreateAuthorDto
{
    public List<Guid> BookIds { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorImage { get; set; } = string.Empty;
}

public class GetAuthorDto
{
    public Guid AuthorId { get; init; }
    public List<Book> Books { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorImage { get; set; } = string.Empty;
}

public class UpdateAuthorDto
{
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorImage { get; set; } = string.Empty;
}