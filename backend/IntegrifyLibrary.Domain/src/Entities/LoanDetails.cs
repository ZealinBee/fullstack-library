namespace IntegrifyLibrary.Domain
{
    public record LoanDetails
    {
        // The reason for this class instead of a simple one to many book -> loans is because in the future I am planning to add more details to the loan, and one -> many relationship is not enough. Also this way I don't have to deal with many to many relationships.
        public Guid LoanDetailsId { get; init; }
        public Guid LoanId { get; init; }
        public Loan Loan { get; init; }
        public Guid BookId { get; init; }
        public Book Book { get; init; }
    }
}