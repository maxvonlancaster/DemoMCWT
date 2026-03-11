using DemoRest.Models;

namespace DemoRest.Services;

public interface IAuthorService
{
    void AddAuthor(Author author);
    Author? GetAuthor(int id);
    IEnumerable<Author> GetAuthors();
    IEnumerable<Author> GetAuthorsByName(string searchWord);
    void RemoveAuthor(Author author);
    Author UpdateAuthor(Author author);
}
