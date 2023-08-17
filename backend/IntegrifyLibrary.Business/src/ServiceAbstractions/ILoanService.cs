using IntegrifyLibrary.Domain;

namespace IntegrifyLibrary.Business;

public interface ILoanService : IBaseService<CreateLoanDto, GetLoanDto, UpdateLoanDto>
{
    Task<CreateLoanDto> CreateLoan(CreateLoanDto dto, Guid userId);

}