using IntegrifyLibrary.Domain;

namespace IntegrifyLibrary.Business;

public interface IBaseService<TCreateDto, TGetDto, TUpdateDto>
{
    Task<TCreateDto> CreateOne(TCreateDto dto);
    Task<TGetDto> GetOne(Guid id);
    Task<List<TGetDto>> GetAll(QueryOptions queryOptions);
    Task<TUpdateDto> UpdateOne(Guid id, TUpdateDto dto);
    Task<bool> DeleteOne(Guid id);
}