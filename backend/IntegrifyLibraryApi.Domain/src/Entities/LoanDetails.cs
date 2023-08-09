namespace IntegrifyLibraryApi.Domain
{
    public record LoanDetails
    {
        public Guid LoanId { get; init; }
        public Guid BookId { get; init; }
    }
}