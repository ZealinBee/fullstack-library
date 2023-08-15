namespace IntegrifyLibrary.Domain;

public interface IBaseRepo<T>
{
    T CreateOne(T item);
    List<T> GetAll();
    T GetOne(Guid id);
    T UpdateOne(T item);
    bool DeleteOne(T item);
}