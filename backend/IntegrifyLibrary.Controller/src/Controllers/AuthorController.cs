using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace IntegrifyLibrary.Controllers;
[ApiController]
public class AuthorController : BaseController<Author, CreateAuthorDto, GetAuthorDto, UpdateAuthorDto>
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService) : base(authorService)
    {
        _authorService = authorService;
    }


}