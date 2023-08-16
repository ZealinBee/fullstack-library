using IntegrifyLibrary.Domain;

using AutoMapper;

namespace IntegrifyLibrary.Business;
public class LoanService : BaseService<Loan, CreateLoanDto, GetLoanDto, UpdateLoanDto>, ILoanService
{
    public LoanService(ILoanRepo loanRepo, IMapper mapper) : base(loanRepo, mapper)
    {

    }
}