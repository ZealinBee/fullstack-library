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
        foreach (var bookId in dto.BookIds)
        {
            var existingBook = await _bookRepo.GetOne(bookId);
            Console.WriteLine("existingBook: " + existingBook.BookName);
            Console.WriteLine("existingBook: " + existingBook.BookId);
            Console.WriteLine("existingBook: " + existingBook.AuthorName);
            Console.WriteLine("existingBook: " + existingBook);
            newLoan.LoanDetails.Add(new LoanDetails
            {
                LoanDetailsId = Guid.NewGuid(),
                LoanId = newLoan.LoanId,
                Loan = newLoan,
                Book = existingBook
            });
            Console.WriteLine("newLoan.LoanDetails: " + newLoan.LoanDetails);
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
}