namespace DemoRest.Queries;

public class GetAuthorsQuery
{
    public int Skip { get; set; }
    public int Take { get; set; }
    public string? SearchWord { get; set; }
}
