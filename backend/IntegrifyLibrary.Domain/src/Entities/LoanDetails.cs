namespace IntegrifyLibrary.Domain
{
    public record LoanDetails
    {
        public Guid LoanDetailsId { get; init; }
        public Guid LoanId { get; init; }
        public Loan Loan { get; init; }
        public Guid BookId { get; init; }
        public Book Book { get; init; }
    }
}