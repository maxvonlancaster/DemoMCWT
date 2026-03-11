using DemoGraphQL.Models;
using DemoGraphQL.Services;

namespace DemoGraphQL;

public class Query
{
    public IEnumerable<Author> GetAuthors(
        [Service] IAuthorService authorService,
        int skip, int take)
    {
        return authorService.GetAuthors(skip, take);
    }

    public IEnumerable<Author> GetAuthorsByName(
        [Service] IAuthorService authorService,
        string searchWord)
    {
        return authorService.GetAuthorsByName(searchWord);
    }
}
