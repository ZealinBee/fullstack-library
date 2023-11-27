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
    private readonly INotificationService _notificationService;
    private readonly IReservationService _reservationService;

    public LoanService(ILoanRepo loanRepo, IMapper mapper, IBookRepo bookRepo, INotificationService notificationService, IReservationService reservationService) : base(loanRepo, mapper)
    {
        _repo = loanRepo;
        _mapper = mapper;
        _bookRepo = bookRepo;
        _notificationService = notificationService;
        _reservationService = reservationService;
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
            if(existingBook.Quantity == 0) {
                throw new Exception("Book is not available");
            }
            existingBook.Quantity--;
            existingBook.LoanedTimes++;
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
        foreach (var loanDetail in loan.LoanDetails)
        {
            var book = await _bookRepo.GetOne(loanDetail.BookId);
            book.Quantity++;
            // if the quantity is 1, then the book went from not available to available
            if(book.Quantity == 1) {
                // check if there is a reservation for this book
                var reservations = await _reservationService.GetAll(queryOptions: new QueryOptions());
                foreach (var reservation in reservations)
                {
                    if(reservation.BookId == book.BookId) {
                        // if there is a reservation, then send a notification to the user
                        NotificationDto notificationDto = new NotificationDto();
                        notificationDto.NotificationMessage = "Book " + book.BookName + " is now available";
                        notificationDto.NotificationType = "BookAvailable";
                        notificationDto.UserId = reservation.UserId;
                        notificationDto.NotificationData.Add("BookId", book.BookId.ToString());
                        await _notificationService.CreateOne(notificationDto);
                        await _reservationService.DeleteOne(reservation.ReservationId);
                    }
                }
            }
        }   
        loan.ReturnedDate = DateOnly.FromDateTime(DateTime.Now);
        return _mapper.Map<GetLoanDto>(await _repo.UpdateOne(loan));
    }
}