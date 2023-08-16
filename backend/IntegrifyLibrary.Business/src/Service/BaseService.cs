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

    public virtual TCreateDto CreateOne(TCreateDto dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));
        var newItem = _mapper.Map<T>(dto);
        return _mapper.Map<TCreateDto>(_repo.CreateOne(newItem));
    }

    public virtual TGetDto GetOne(Guid id)
    {
        return _mapper.Map<TGetDto>(_repo.GetOne(id));
    }

    public virtual List<TGetDto> GetAll(QueryOptions queryOptions)
    {
        var result = _repo.GetAll(queryOptions);
        return _mapper.Map<List<TGetDto>>(_repo.GetAll(queryOptions));
    }

    public virtual TUpdateDto UpdateOne(Guid id, TUpdateDto itemToUpdate)
    {
        var foundItem = _repo.GetOne(id);
        var updatedItem = _mapper.Map(itemToUpdate, foundItem);
        _repo.UpdateOne(updatedItem);
        return _mapper.Map<TUpdateDto>(updatedItem);
    }


    public virtual bool DeleteOne(Guid id)
    {
        var item = _repo.GetOne(id);
        return _repo.DeleteOne(item);
    }
}