namespace IntegrifyLibrary.Domain

{
    public record Loan
    {
        public Guid LoanId { get; init; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateOnly LoanDate { get; init; }
        public DateOnly DueDate { get; set; }
        public DateOnly ReturnedDate { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateOnly ModifiedAt { get; set; }
        public List<LoanDetails> LoanDetails { get; set; } = new List<LoanDetails>();
    }
}
