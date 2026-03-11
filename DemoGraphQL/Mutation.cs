using DemoGraphQL.Models;
using DemoGraphQL.Services;

namespace DemoGraphQL;

public class Mutation
{
    public string AddAuthor(
        [Service] IAuthorService authorService,
        string name, string country)
    {
        authorService.AddAuthor(
            new Author
        {
            Name = name,
            Country = country
        });

        return $"Author '{name}' from '{country}' added successfully!";
    }
}
