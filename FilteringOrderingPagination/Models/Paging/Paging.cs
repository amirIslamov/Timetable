namespace FilteringOrderingPagination.Models.Paging;

public class Paging
{
    public static readonly Paging Unpaged = new();

    public Paging(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;

        IsUnpaged = false;
    }

    private Paging()
    {
        PageNumber = 0;
        PageSize = 0;

        IsUnpaged = true;
    }

    public long Offset => PageSize * PageNumber;
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public bool IsUnpaged { get; init; }
}