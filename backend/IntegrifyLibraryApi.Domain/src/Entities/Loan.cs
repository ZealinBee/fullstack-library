namespace IntegrifyLibraryApi.Domain
{
    public record Loan
    {
        public Guid LoanId { get; init; }
        public Guid BookId { get; init; }
        public DateOnly LoanDate { get; init; }
        public DateOnly DueDate { get; init; }
        public DateOnly ReturnedDate { get; init; }
        public DateOnly CreatedAt { get; init; }
        public DateOnly ModifiedAt { get; init; }
    }
}
