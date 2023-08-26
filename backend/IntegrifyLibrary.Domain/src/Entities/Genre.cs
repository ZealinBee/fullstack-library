namespace IntegrifyLibrary.Domain
{
    public record Genre
    {
        public Guid GenreId { get; init; }
        public string GenreName { get; init; } = string.Empty;
        public DateOnly CreatedAt { get; init; }
        public DateOnly ModifiedAt { get; init; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }

}
