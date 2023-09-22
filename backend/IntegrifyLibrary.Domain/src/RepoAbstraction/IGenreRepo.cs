namespace IntegrifyLibrary.Domain;

public interface IGenreRepo : IBaseRepo<Genre>
{
    Task<Genre> GetOneByGenreName(string genreName);
}