namespace IntegrifyLibrary.Domain
{
    public record Genre
    {
        public Guid GenreId { get; init; }
        public string Name { get; init; }
        public DateOnly CreatedAt { get; init; }
        public DateOnly ModifiedAt { get; init; }
    }

}
