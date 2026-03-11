using DemoRest.Models;
using DemoRest.Queries;
using DemoRest.Services;

namespace DemoRest.Handlers;

public class GetAuthorsQueryHandler
{
    private readonly IAuthorService _authorService;

    public GetAuthorsQueryHandler(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    public IEnumerable<Author> Handle(GetAuthorsQuery query)
    {
        var authorsQueriable = _authorService.GetAuthors()
            .Skip(query.Skip).Take(query.Take);

        if (!string.IsNullOrEmpty(query.SearchWord)) 
        {
            authorsQueriable = authorsQueriable
                .Where(a => a.Name.Contains(query.SearchWord, StringComparison.OrdinalIgnoreCase));
        }

        return authorsQueriable.AsEnumerable();
    }
}
