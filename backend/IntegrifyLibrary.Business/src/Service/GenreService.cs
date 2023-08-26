using IntegrifyLibrary.Domain;
using AutoMapper;

namespace IntegrifyLibrary.Business;

public class GenreService : BaseService<Genre, CreateGenreDto, GetGenreDto, UpdateGenreDto>, IGenreService
{
    public GenreService(IGenreRepo repo, IMapper mapper) : base(repo, mapper)
    {
    }
}