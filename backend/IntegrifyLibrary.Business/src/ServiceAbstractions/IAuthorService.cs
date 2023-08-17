using IntegrifyLibrary.Domain;

namespace IntegrifyLibrary.Business
{
    public interface IAuthorService : IBaseService<CreateAuthorDto, GetAuthorDto, UpdateAuthorDto>
    {
    }
}