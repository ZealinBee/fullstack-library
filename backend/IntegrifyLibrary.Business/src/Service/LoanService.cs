using IntegrifyLibrary.Domain;

using AutoMapper;
using System.Security.Claims;

namespace IntegrifyLibrary.Business;
public class LoanService : BaseService<Loan, CreateLoanDto, GetLoanDto, UpdateLoanDto>, ILoanService
{
    private readonly ILoanRepo _repo;
    private readonly IMapper _mapper;
    public LoanService(ILoanRepo loanRepo, IMapper mapper) : base(loanRepo, mapper)
    {
        _repo = loanRepo;
        _mapper = mapper;
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

    public async Task<List<GetLoanDto>> GetOwnLoans(Guid userId)
    {
        QueryOptions queryOptions = new QueryOptions();
        var loans = await _repo.GetAll(queryOptions);
        List<GetLoanDto> ownLoans = new List<GetLoanDto>();
        Console.WriteLine("loans: " + loans);
        foreach (var loan in loans)
        {
            if (loan.UserId == userId)
            {
                ownLoans.Add(_mapper.Map<GetLoanDto>(loan));
            }
        }
        return ownLoans;
    }
}