using DemoRest.Commands;
using DemoRest.Handlers;
using DemoRest.Models;
using DemoRest.Queries;
using DemoRest.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoRest.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;
    private readonly AddAuthorHandler _addAuthorHandler;
    private readonly GetAuthorsQueryHandler _getAuthorsQueryHandler;

    public AuthorController(
        IAuthorService authorService,
        AddAuthorHandler addAuthorHandler,
        GetAuthorsQueryHandler getAuthorsQueryHandler)
    {
        _authorService = authorService;
        _addAuthorHandler = addAuthorHandler;
        _getAuthorsQueryHandler = getAuthorsQueryHandler;
    }

    [HttpPost(Name = "authors")] // Post
    public IEnumerable<Author> Authors(GetAuthorsQuery query)
    {
        return _getAuthorsQueryHandler.Handle(query);
    }

    [HttpPost(Name = "add-author")] // POST /author/add-author
    public IActionResult AddAuthor(AddAuthorCommand command)
    {
        _addAuthorHandler.Handle(command);
        return Ok("added succsesfully");
    }



    // TODO: refactor using CQRS pattern
    [HttpGet(Name = "get-author")] // GET /author/get-author?id=1
    public Author GetAuthor(int id)
    {
        var author = _authorService.GetAuthor(id);

        if(author == null)
        {
            throw new KeyNotFoundException($"Author with id {id} not found.");
        }
        return author;
    }

    [HttpPost(Name = "update-author")] // POST /author/update-author
    public Author UpdateAuthor(Author author)
    {
        return _authorService.UpdateAuthor(author);
    }

    [HttpPost(Name = "remove-author")] // POST /author/remove-author
    public void RemoveAuthor(Author author)
    {
        _authorService.RemoveAuthor(author);
    }
}
