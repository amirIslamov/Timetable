using System.Linq.Expressions;
using FilteringOrderingPagination.Models.Expressions;
using FilteringOrderingPagination.Models.Specifications;

namespace FilteringOrderingPagination.Models;

public class DatePattern : ValuePropertyPattern<DateTime>
{
    public DateTime? Before { get; set; }
    public DateTime? After { get; set; }

    public override Specification<TEntity> AppliedTo<TEntity>(Expression<Func<TEntity, DateTime>> selector)
    {
        Specification<TEntity> resultSpec = null;

        if (Before != null)
        {
            var eqSpec = new Specification<TEntity>(selector.Chain(x => x <= Before));
            resultSpec = resultSpec == null
                ? eqSpec
                : new AndSpecification<TEntity>(resultSpec, eqSpec);
        }

        if (After != null)
        {
            var neqSpec = new Specification<TEntity>(selector.Chain(x => x >= After));
            resultSpec = resultSpec == null
                ? neqSpec
                : new AndSpecification<TEntity>(resultSpec, neqSpec);
        }

        if (resultSpec == null) resultSpec = new Specification<TEntity>(x => true);

        return new AndSpecification<TEntity>(resultSpec, base.AppliedTo(selector));
    }
}