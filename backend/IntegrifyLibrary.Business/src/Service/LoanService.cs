using IntegrifyLibrary.Domain;

using AutoMapper;
using System.Security.Claims;

namespace IntegrifyLibrary.Business;
public class LoanService : BaseService<Loan, CreateLoanDto, GetLoanDto, UpdateLoanDto>, ILoanService
{
    public LoanService(ILoanRepo loanRepo, IMapper mapper) : base(loanRepo, mapper)
    {

    }

    public async Task<CreateLoanDto> CreateLoan(CreateLoanDto dto, Guid userId)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));
        var newLoan = _mapper.Map<Loan>(dto);
        newLoan.UserId = userId;
        foreach (var bookId in dto.BookIds)
        {
            newLoan.LoanDetails.Add(new LoanDetails
            {
                LoanDetailsId = Guid.NewGuid(),
                LoanId = newLoan.LoanId,
                Loan = newLoan,
                BookId = bookId,
            });
        }
        return _mapper.Map<CreateLoanDto>(await _repo.CreateOne(newLoan));
    }
}