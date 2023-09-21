namespace IntegrifyLibrary.Domain
{
    public class Author
    {
        public Guid AuthorId { get; init; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
        public string AuthorName { get; set; } = string.Empty;
        public DateOnly CreatedAt { get; set; }
        public DateOnly ModifiedAt { get; set; }
        public string AuthorImage { get; set; } = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_640.png";
    }
}
