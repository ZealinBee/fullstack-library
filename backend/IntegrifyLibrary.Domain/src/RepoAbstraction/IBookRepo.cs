namespace IntegrifyLibrary.Domain;

public interface IBookRepo : IBaseRepo<Book>
{
    Task<Book> GetOneByBookName(string bookName);
}