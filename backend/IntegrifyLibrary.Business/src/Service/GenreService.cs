using IntegrifyLibrary.Domain;
using AutoMapper;

namespace IntegrifyLibrary.Business;

public class GenreService : BaseService<Genre, CreateGenreDto, GetGenreDto, UpdateGenreDto>, IGenreService
{
    private readonly IGenreRepo _genreRepo;
    private readonly IMapper _mapper;
    public GenreService(IGenreRepo repo, IMapper mapper) : base(repo, mapper)
    {
        _genreRepo = repo;
        _mapper = mapper;
    }

    public async Task<GetGenreDto> CreateOne(CreateGenreDto dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));
        if (await _genreRepo.GetOneByGenreName(dto.GenreName) != null) throw new Exception($"Genre with name {dto.GenreName} already exists");

        var newGenre = _mapper.Map<Genre>(dto);
        return _mapper.Map<GetGenreDto>(await _genreRepo.CreateOne(newGenre));
    }
}