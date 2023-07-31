namespace IntegrifyLibrary.Entities
{
    public record Loan
    {
        public Guid LoanId { get; init; }
        public Guid BookId { get; init; }
        public DateTime LoanDate { get; init; }
        public DateTime DueDate { get; init; }
        public DateTime ReturnedDate { get; init; }
        public Timestamp CreatedAt { get; init; }
        public Timestamp ModifiedAt { get; init; }
    }
}
