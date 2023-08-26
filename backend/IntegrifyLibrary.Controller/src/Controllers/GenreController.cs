using IntegrifyLibrary.Business;
using IntegrifyLibrary.Domain;

using Microsoft.AspNetCore.Mvc;
namespace IntegrifyLibrary.Controllers;

[ApiController]

public class GenreController : BaseController<Genre, CreateGenreDto, GetGenreDto, UpdateGenreDto>
{
    private readonly IGenreService _genreService;
    public GenreController(IGenreService service) : base(service)
    {
        _genreService = service;
    }
}