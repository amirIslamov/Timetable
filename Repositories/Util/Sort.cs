using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repositories.Util
{
    public class Sort<T>
    {
        public enum Direction
        {
            Ascending,
            Descending
        }

        public class SortingStage<T>
        {
            public Expression<Func<T, object>> PropertyExpression { get; set; }
            public Direction Direction { get; set; }

            public SortingStage(Expression<Func<T, object>> propertyExpression, Direction direction)
            {
                PropertyExpression = propertyExpression;
                Direction = direction;
            }
        }
        
        Sort() {}

        public IList<SortingStage<T>> Stages { get; private set; }

        public static Sort<T> By(string propertyName, Direction direction) 
            => throw new NotImplementedException();
        public static Sort<T> By<TProperty>(Expression<Func<T, TProperty>> propertyExpression, Direction direction) 
            => throw new NotImplementedException();
        
        public Sort<T> ThenBy(string propertyName, Direction direction) 
            => throw new NotImplementedException();
        public Sort<T> ThenBy<TProperty>(Expression<Func<T, TProperty>> propertyExpression, Direction direction) 
            => throw new NotImplementedException();
    }
}