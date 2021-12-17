namespace FilteringOrderingPagination.Models;

public class RelationalPattern<T> : ValuePropertyPattern<T> where T : struct
{
    public T Lt { get; set; }
    public T Gt { get; set; }
}