using IntegrifyLibrary.Domain;

using AutoMapper;

namespace IntegrifyLibrary.Business
{
    public class AuthorService : BaseService<Author, CreateAuthorDto, GetAuthorDto, UpdateAuthorDto>, IAuthorService
    {
        public AuthorService(IAuthorRepo authorRepo, IMapper mapper) : base(authorRepo, mapper)
        {
        }
    }
}