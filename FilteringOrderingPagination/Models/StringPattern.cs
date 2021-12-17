namespace FilteringOrderingPagination.Models;

public class StringPattern : ReferencePropertyPattern<string>
{
    public string Contains { get; set; }
    public string StartsWith { get; set; }
    public string EndsWith { get; set; }
}