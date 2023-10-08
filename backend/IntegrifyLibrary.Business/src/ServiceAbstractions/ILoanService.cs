using IntegrifyLibrary.Domain;

namespace IntegrifyLibrary.Business;

public interface ILoanService : IBaseService<CreateLoanDto, GetLoanDto, UpdateLoanDto>
{
    Task<GetLoanDto> CreateLoan(CreateLoanDto dto, Guid userId);
    Task<List<GetLoanDto>> GetOwnLoans(Guid userId);
    Task<GetLoanDto> ReturnLoan(Guid loanId);

}