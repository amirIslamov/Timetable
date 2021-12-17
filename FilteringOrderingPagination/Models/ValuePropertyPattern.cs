using System.Linq.Expressions;
using FilteringOrderingPagination.Models.Expressions;
using FilteringOrderingPagination.Models.Specifications;

namespace FilteringOrderingPagination.Models;

public class ValuePropertyPattern<T>
    where T : struct
{
    public T? Eq { get; set; }
    public T? Neq { get; set; }
    public IList<T> OneOf { get; set; }

    public virtual Specification<TEntity> AppliedTo<TEntity>(Expression<Func<TEntity, T>> selector)
    {
        Specification<TEntity> resultSpec = null;

        if (Eq != null)
        {
            var eqSpec = new Specification<TEntity>(selector.Chain(x => x.Equals(Eq)));
            resultSpec = resultSpec == null
                ? eqSpec
                : new AndSpecification<TEntity>(resultSpec, eqSpec);
        }

        if (Neq != null)
        {
            var neqSpec = new Specification<TEntity>(selector.Chain(x => !x.Equals(Neq)));
            resultSpec = resultSpec == null
                ? neqSpec
                : new AndSpecification<TEntity>(resultSpec, neqSpec);
        }

        if (OneOf != null)
        {
            var oneOfSpec = new Specification<TEntity>(selector.Chain(x => OneOf.Contains(x)));
            resultSpec = resultSpec == null
                ? oneOfSpec
                : new AndSpecification<TEntity>(resultSpec, oneOfSpec);
        }

        if (resultSpec == null) resultSpec = new Specification<TEntity>(x => true);

        return resultSpec;
    }
}