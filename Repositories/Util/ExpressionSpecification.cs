using System;
using System.Linq.Expressions;

namespace Repositories.Util
{
    public abstract class ExpressionSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T , bool>> Expression { get; }
 
        public ExpressionSpecification(Expression<Func<T , bool>> expression)
        {
            Expression = expression;
        }
 
        public bool IsSatisfied(T entity)
        {
            return Expression.Compile().Invoke(entity);
        }
    }
}