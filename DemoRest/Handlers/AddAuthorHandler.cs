using DemoRest.Commands;
using DemoRest.Models;
using DemoRest.Services;

namespace DemoRest.Handlers;

public class AddAuthorHandler
{
    private readonly IAuthorService _authorService;

    public AddAuthorHandler(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    public void Handle(AddAuthorCommand request)
    {
        if (request.Country == "UK") 
        {
            throw new Exception("We do not add authors from UK");
        }

        var author = new Author
        {
            Name = request.Name,
            Country = request.Country
        };

        _authorService.AddAuthor(author);
    }
}
