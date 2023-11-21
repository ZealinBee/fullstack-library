namespace IntegrifyLibrary.Business;

public class BookDto
{
    public string BookName { get; set; }
    public string AuthorName { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public int Quantity { get; set; }
    public int PageCount { get; set; }
    public DateOnly PublishedDate { get; set; }
    public string BookImage { get; set; }
    public string GenreName { get; set; }
    public int loanedTimes { get; set; }
}

public class GetBookDto : BookDto
{
    public Guid BookId { get; set; }
    public Guid AuthorId { get; set; }
}