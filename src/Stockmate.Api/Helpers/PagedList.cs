namespace Stockmate.Api.Helpers;

public class PagedList<T>
{
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalPages { get; }
    public int TotalItems { get; }
    public List<T> Items { get; }

    public PagedList(List<T> items, int pageNumber, int pageSize, int totalItems)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
        TotalItems = totalItems;
        Items = items;
    }
}
