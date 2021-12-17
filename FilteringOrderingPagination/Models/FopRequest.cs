namespace FilteringOrderingPagination.Models;

public class FopRequest<T, TFilter>
    where TFilter : IFilter<T>
{
    public TFilter Filter { get; set; }
    public Paging.Paging Paging { get; set; }
}