namespace Repositories.Util
{
    public class Paging
    {
        public long Offset => PageSize * PageNumber;
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public bool IsUnpaged { get; init; }

        public Paging(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;

            IsUnpaged = false;
        }

        Paging()
        {
            PageNumber = 0;
            PageSize = 0;

            IsUnpaged = true;
        }

        public static readonly Paging Unpaged = new Paging();
    }
}