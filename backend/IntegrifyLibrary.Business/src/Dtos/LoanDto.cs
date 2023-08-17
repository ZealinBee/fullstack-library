namespace IntegrifyLibrary.Business;
public class GetLoanDto
{
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
    public DateOnly LoanDate { get; set; }
    public DateOnly DueDate { get; set; }
    public DateOnly ReturnedDate { get; set; }
}

public class CreateLoanDto
{
    public List<Guid> BookIds { get; set; }
    public DateOnly LoanDate { get; set; }
}

public class UpdateLoanDto
{
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
    public DateOnly LoanDate { get; set; }
    public DateOnly DueDate { get; set; }
    public DateOnly ReturnedDate { get; set; }
}