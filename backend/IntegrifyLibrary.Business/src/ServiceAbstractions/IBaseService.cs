using IntegrifyLibrary.Domain;

namespace IntegrifyLibrary.Business;

public interface IBaseService<TCreateDto, TGetDto, TUpdateDto>
{
    TCreateDto CreateOne(TCreateDto dto);
    TGetDto GetOne(Guid id);
    List<TGetDto> GetAll();
    TUpdateDto UpdateOne(Guid id, TUpdateDto dto);
    bool DeleteOne(Guid id);
}