using IntegrifyLibrary.Domain;

using AutoMapper;

namespace IntegrifyLibrary.Business;

public class BookService : BaseService<Book, BookDto, GetBookDto, BookDto>, IBookService
{
    private readonly IBookRepo _bookRepo;
    private readonly IAuthorRepo _authorRepo;
    private readonly IMapper _mapper;
    public BookService(IBookRepo bookRepo, IMapper mapper, IAuthorRepo authorRepo) : base(bookRepo, mapper)
    {
        _bookRepo = bookRepo;
        _authorRepo = authorRepo;
        _mapper = mapper;
    }

    public async Task<BookDto> CreateOne(BookDto dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));
        if (await _bookRepo.GetOneByBookName(dto.BookName) != null) throw new Exception($"Book with name {dto.BookName} already exists");

        var existingAuthor = await _authorRepo.GetOneByAuthorName(dto.AuthorName);
        if (existingAuthor == null)
        {
            var newAuthor = new Author { AuthorName = dto.AuthorName, CreatedAt = DateOnly.FromDateTime(DateTime.Now), ModifiedAt = DateOnly.FromDateTime(DateTime.Now), AuthorId = Guid.NewGuid() };
            await _authorRepo.CreateOne(newAuthor);
            var newBook = _mapper.Map<Book>(dto);
            newBook.AuthorId = newAuthor.AuthorId;
            return _mapper.Map<BookDto>(await _repo.CreateOne(newBook));
        }
        else
        {
            var newBook = _mapper.Map<Book>(dto);
            newBook.AuthorId = existingAuthor.AuthorId;
            return _mapper.Map<BookDto>(await _repo.CreateOne(newBook));
        }
    }
}