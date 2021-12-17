using FilteringOrderingPagination.Models.Specifications;

namespace FilteringOrderingPagination.Models;

public interface IFilter<T>
{
    Specification<T> ToSpecification();
}