namespace IntegrifyLibrary.Entities
{
    public record Book
    {
        public Guid Id { get; init; }
        public string BookName { get; init; }
        public string ISBN { get; init; }
        public string AuthorName { get; init; }
        public Guid GenreId { get; init; }
        public Guid AuthorId { get; init; }
        public int Quantity { get; init; }
        public int PageCount { get; init; }
        public DateTime PublishedDate { get; init; }
        public Timestamp CreatedAt { get; init; }
        public Timestamp ModifiedAt { get; init; }
    }
}
