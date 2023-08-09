namespace IntegrifyLibrary.Domain
{
    public record Author
    {
        public Guid Id { get; init; }
        public Guid BookId { get; init; }
        public string AuthorName { get; init; }
    }
}
