namespace IntegrifyLibrary.Domain
{
    public class Author
    {
        public Guid Id { get; init; }
        public List<Guid> BookIds { get; set; }
        public string AuthorName { get; set; } = string.Empty;
    }
}
