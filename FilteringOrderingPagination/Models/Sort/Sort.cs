using System.Linq.Expressions;

namespace FilteringOrderingPagination.Models.Sort;

public class Sort<T>
{
    public enum Direction
    {
        Ascending,
        Descending
    }

    private Sort()
    {
    }

    public static Sort<T> Unordered { get; } = new();

    public IList<SortingStage<T>> Stages { get; private set; }

    public static Sort<T> By<TProperty>(string propertyName, Direction direction)
    {
        throw new NotImplementedException();
    }

    public Sort<T> ThenBy<TProperty>(string propertyExpression, Direction direction)
    {
        throw new NotImplementedException();
    }

    public class SortingStage<T>
    {
        public SortingStage(Expression<Func<T, object>> propertyExpression, Direction direction)
        {
            PropertyExpression = propertyExpression;
            Direction = direction;
        }

        public Expression<Func<T, object>> PropertyExpression { get; set; }
        public Direction Direction { get; set; }
    }
}