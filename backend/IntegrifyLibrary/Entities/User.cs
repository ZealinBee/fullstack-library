namespace IntegrifyLibrary.Entities
{
    public record User
    {
        public Guid UserId { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; init; }
        public byte[] Password { get; set; }
        public bool IsLibrarian { get; set; }
        public DateOnly CreatedAt { get; init; }
        public DateOnly UpdatedAt { get; set; }
    }
}
