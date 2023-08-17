namespace IntegrifyLibrary.Domain;

public interface IAuthorRepo : IBaseRepo<Author>
{
    Task<Author> GetOneByAuthorName(string authorName);
}