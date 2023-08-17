namespace IntegrifyLibrary.Domain
{
    public class Book
    {
        public Guid Id { get; init; }
        public string BookName { get; set; }
        public string ISBN { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public Guid GenreId { get; set; }
        public Guid AuthorId { get; set; }
        public int Quantity { get; set; }
        public int PageCount { get; set; }
        public DateOnly PublishedDate { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateOnly ModifiedAt { get; set; }
    }
}
