namespace IntegrifyLibrary.Domain;

public class QueryOptions
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string Filter { get; set; } = "";
    public string FilterBy { get; set; } = "Title";
    public string OrderBy { get; set; } = "Title";
    public string OrderByDirection { get; set; } = "asc";

}