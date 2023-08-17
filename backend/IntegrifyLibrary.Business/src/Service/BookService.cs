using IntegrifyLibrary.Domain;

using AutoMapper;

namespace IntegrifyLibrary.Business;

public class BookService : BaseService<Book, BookDto, BookDto, BookDto>, IBookService
{
    private readonly IBookRepo _bookRepo;
    public BookService(IBookRepo bookRepo, IMapper mapper) : base(bookRepo, mapper)
    {
        _bookRepo = bookRepo;
    }

    public async Task<BookDto> CreateOne(BookDto dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));
        if (await _bookRepo.GetOneByBookName(dto.BookName) != null) throw new Exception($"Book with name {dto.BookName} already exists");
        var newItem = _mapper.Map<Book>(dto);
        return _mapper.Map<BookDto>(await _repo.CreateOne(newItem));
    }
}