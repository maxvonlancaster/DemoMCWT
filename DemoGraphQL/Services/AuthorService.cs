using DemoGraphQL.Models;

namespace DemoGraphQL.Services;

public class AuthorService : IAuthorService
{
    private readonly List<Author> _authors;

    public AuthorService()
    {
        _authors = new List<Author>
            {
                new Author
                {
                    Id = 1,
                    Name = "J.K. Rowling",
                    Country = "United Kingdom",
                    Books = new List<Book>
                    {
                        new Book { Id = 1, Title = "Harry Potter and the Sorcerer's Stone", Description = "The first book in the Harry Potter series." },
                        new Book { Id = 2, Title = "Harry Potter and the Chamber of Secrets", Description = "The second book in the Harry Potter series." }
                    }
                },
                new Author
                {
                    Id = 2,
                    Name = "George R.R. Martin",
                    Country = "United States",
                    Books = new List<Book>
                    {
                        new Book { Id = 3, Title = "A Game of Thrones", Description = "The first book in the A Song of Ice and Fire series." },
                        new Book { Id = 4, Title = "A Clash of Kings", Description = "The second book in the A Song of Ice and Fire series." }
                    }
                },
                new Author
                {
                    Id = 3,
                    Name = "Haruki Murakami",
                    Country = "Japan",
                    Books = new List<Book>
                    {
                        new Book { Id = 5, Title = "Norwegian Wood", Description = "A novel about love and loss." },
                        new Book { Id = 6, Title = "Kafka on the Shore", Description = "A surreal novel blending reality and fantasy." }
                    }
                },
                new Author
                {
                    Id = 4,
                    Name = "Isabel Allende",
                    Country = "Chile",
                    Books = new List<Book>
                    {
                        new Book { Id = 7, Title = "The House of the Spirits", Description = "A family saga spanning generations." },
                        new Book { Id = 8, Title = "Eva Luna", Description = "A novel about a storyteller's life." }
                    }
                },
                new Author
                {
                    Id = 5,
                    Name = "Chinua Achebe",
                    Country = "Nigeria",
                    Books = new List<Book>
                    {
                        new Book { Id = 9, Title = "Things Fall Apart", Description = "A novel about the impact of colonialism on African society." },
                        new Book { Id = 10, Title = "No Longer at Ease", Description = "A novel about a Nigerian" }
                    }
                },
                new Author{
                    Id = 6,
                    Name = "Jane Austen",
                    Country = "United Kingdom",
                    Books = new List<Book>
                    {
                        new Book { Id = 11, Title = "Pride and Prejudice", Description = "A romantic novel about manners and marriage." },
                        new Book { Id = 12, Title = "Sense and Sensibility", Description = "A novel about the Dashwood sisters and their romantic entanglements." }
                    }
                }
            };

    }

    public IEnumerable<Author> GetAuthors(int skip, int take)
    {
        return _authors.Skip(skip).Take(take);
    }

    public IEnumerable<Author> GetAuthorsByName(string searchWord)
    {
        return _authors
            .Where(x => x.Name.ToLower().Contains(searchWord.ToLower()));
    }

    public void AddAuthor(Author author)
    {
        author.Id = _authors.Count + 1;
        _authors.Add(author);
    }
}
