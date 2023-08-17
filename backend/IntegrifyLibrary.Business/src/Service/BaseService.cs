using AutoMapper;
using IntegrifyLibrary.Domain;

namespace IntegrifyLibrary.Business;

public class BaseService<T, TCreateDto, TGetDto, TUpdateDto> : IBaseService<TCreateDto, TGetDto, TUpdateDto>
{
    protected readonly IMapper _mapper;
    protected readonly IBaseRepo<T> _repo;

    public BaseService(IBaseRepo<T> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public virtual async Task<TCreateDto> CreateOne(TCreateDto dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));
        var newItem = _mapper.Map<T>(dto);
        return _mapper.Map<TCreateDto>(_repo.CreateOne(newItem));
    }

    public virtual async Task<TGetDto> GetOne(Guid id)
    {
        return _mapper.Map<TGetDto>(_repo.GetOne(id));
    }

    public virtual async Task<List<TGetDto>> GetAll(QueryOptions queryOptions)
    {
        var result = await _repo.GetAll(queryOptions);
        return _mapper.Map<List<TGetDto>>(_repo.GetAll(queryOptions));
    }

    public virtual async Task<TUpdateDto> UpdateOne(Guid id, TUpdateDto itemToUpdate)
    {
        var foundItem = await _repo.GetOne(id);
        if (foundItem == null) throw new Exception($"Item with id {id} not found");
        var updatedItem = _mapper.Map(itemToUpdate, foundItem);
        _repo.UpdateOne(updatedItem);
        return _mapper.Map<TUpdateDto>(updatedItem);
    }


    public virtual async Task<bool> DeleteOne(Guid id)
    {
        var item = await _repo.GetOne(id);
        return await _repo.DeleteOne(item);
    }
}