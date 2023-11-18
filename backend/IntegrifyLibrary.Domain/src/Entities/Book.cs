namespace IntegrifyLibrary.Domain
{
    public class Book
    {
        public Guid BookId { get; init; }
        public string BookName { get; set; }
        public string ISBN { get; set; }
        public string AuthorName { get; set; }
        public Author Author { get; set; } 
        public string Description { get; set; } = "No description";
        public Guid AuthorId { get; set; }
        public int Quantity { get; set; } = 0;
        public int PageCount { get; set; }
        public DateOnly PublishedDate { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateOnly ModifiedAt { get; set; } = DateOnly.MinValue;
        public int LoanedTimes { get; set; } = 0;
        public string BookImage { get; set; } = "https://www.pngitem.com/pimgs/m/199-1997941_blank-book-cover-png-transparent-png.png";
        public Genre Genre { get; set; }
        public Guid GenreId { get; set; }
        public string GenreName { get; set; }
        public ICollection<LoanDetails> LoanDetails { get; set; } = new List<LoanDetails>();

    }
}
