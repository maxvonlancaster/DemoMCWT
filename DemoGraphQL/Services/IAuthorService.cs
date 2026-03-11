using DemoGraphQL.Models;

namespace DemoGraphQL.Services;

public interface IAuthorService
{
    void AddAuthor(Author author);
    IEnumerable<Author> GetAuthors(int skip, int take);
    IEnumerable<Author> GetAuthorsByName(string searchWord);
}
