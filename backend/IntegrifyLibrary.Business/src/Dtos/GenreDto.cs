using IntegrifyLibrary.Domain;

namespace IntegrifyLibrary.Business;

public class CreateGenreDto
{
    public string GenreName { get; set; }
}

public class UpdateGenreDto
{
    public string GenreName { get; set; }
}

public class GetGenreDto
{
    public string GenreName { get; set; }
    public ICollection<Book> Books { get; set; }
}