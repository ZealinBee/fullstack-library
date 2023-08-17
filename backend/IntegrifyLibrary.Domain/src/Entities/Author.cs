namespace IntegrifyLibrary.Domain
{
    public class Author
    {
        public Guid AuthorId { get; init; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
        public string AuthorName { get; set; } = string.Empty;
        public DateOnly CreatedAt { get; set; }
        public DateOnly ModifiedAt { get; set; }
    }
}
