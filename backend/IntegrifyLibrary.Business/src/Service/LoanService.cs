using IntegrifyLibrary.Domain;

using AutoMapper;
using System.Security.Claims;
using System.Runtime.CompilerServices;

namespace IntegrifyLibrary.Business;
public class LoanService : BaseService<Loan, CreateLoanDto, GetLoanDto, UpdateLoanDto>, ILoanService
{
    private readonly ILoanRepo _repo;
    private readonly IMapper _mapper;
    private readonly IBookRepo _bookRepo;
    public LoanService(ILoanRepo loanRepo, IMapper mapper, IBookRepo bookRepo) : base(loanRepo, mapper)
    {
        _repo = loanRepo;
        _mapper = mapper;
        _bookRepo = bookRepo;
    }

    public async Task<GetLoanDto> CreateLoan(CreateLoanDto dto, Guid userId)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));
        var newLoan = _mapper.Map<Loan>(dto);
        newLoan.UserId = userId;
        newLoan.DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(10));
        foreach (var bookId in dto.BookIds)
        {
            var existingBook = await _bookRepo.GetOne(bookId);
            newLoan.LoanDetails.Add(new LoanDetails
            {
                LoanDetailsId = Guid.NewGuid(),
                LoanId = newLoan.LoanId,
                Loan = newLoan,
                Book = existingBook
            });
        }
        return _mapper.Map<GetLoanDto>(await _repo.CreateOne(newLoan));
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

    public async Task<GetLoanDto> ReturnLoan(Guid loanId)
    {
        var loan = await _repo.GetOne(loanId);
        if (loan == null) throw new ArgumentNullException(nameof(loan));
        loan.ReturnedDate = DateOnly.FromDateTime(DateTime.Now);
        return _mapper.Map<GetLoanDto>(await _repo.UpdateOne(loan));
    }
}