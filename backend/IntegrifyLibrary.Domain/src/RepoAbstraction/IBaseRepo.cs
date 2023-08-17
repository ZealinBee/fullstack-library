namespace IntegrifyLibrary.Domain;

public interface IBaseRepo<T>
{
    Task<T> CreateOne(T item);
    Task<List<T>> GetAll(QueryOptions queryOptions);
    Task<T> GetOne(Guid id);
    Task<T> UpdateOne(T item);
    Task<bool> DeleteOne(T item);
}