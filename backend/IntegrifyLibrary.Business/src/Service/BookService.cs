using IntegrifyLibrary.Domain;

using AutoMapper;

namespace IntegrifyLibrary.Business;

public class BookService : BaseService<Book, BookDto, GetBookDto, BookDto>, IBookService
{
    private readonly IBookRepo _bookRepo;
    private readonly IAuthorRepo _authorRepo;
    private readonly IGenreRepo _genreRepo;
    private readonly IMapper _mapper;
    public BookService(IBookRepo bookRepo, IMapper mapper, IAuthorRepo authorRepo, IGenreRepo genreRepo) : base(bookRepo, mapper)
    {
        _bookRepo = bookRepo;
        _authorRepo = authorRepo;
        _genreRepo = genreRepo;
        _mapper = mapper;
    }

    public async Task<GetBookDto> CreateOne(BookDto dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        var existingAuthor = await _authorRepo.GetOneByAuthorName(dto.AuthorName);
        var existingGenre = await _genreRepo.GetOneByGenreName(dto.GenreName);

        if (existingAuthor == null)
        {
            var newAuthor = new Author { AuthorName = dto.AuthorName, CreatedAt = DateOnly.FromDateTime(DateTime.Now), ModifiedAt = DateOnly.FromDateTime(DateTime.Now), AuthorId = Guid.NewGuid() };
            await _authorRepo.CreateOne(newAuthor);
            existingAuthor = newAuthor;
        }

        if (existingGenre == null)
        {
            var newGenre = new Genre { GenreName = dto.GenreName, CreatedAt = DateOnly.FromDateTime(DateTime.Now), ModifiedAt = DateOnly.FromDateTime(DateTime.Now), GenreId = Guid.NewGuid() };
            await _genreRepo.CreateOne(newGenre);
            existingGenre = newGenre;
        }

        var newBook = _mapper.Map<Book>(dto);
        newBook.AuthorId = existingAuthor.AuthorId;
        newBook.GenreId = existingGenre.GenreId;

        return _mapper.Map<GetBookDto>(await _bookRepo.CreateOne(newBook));
    }
}