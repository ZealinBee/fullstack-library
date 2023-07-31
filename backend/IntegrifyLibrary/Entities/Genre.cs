namespace IntegrifyLibrary.Entities
{
    public record Genre
    {
        public Guid Genre_id { get; init; }
        public Guid Book_id { get; init; }
        public string Genre_name { get; init; }
        public Timestamp CreatedAt { get; init; }
        public Timestamp ModifiedAt { get; init; }
    }
}