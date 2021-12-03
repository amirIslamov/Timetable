namespace Repositories.Util
{
    public interface ISpecification<in T>
    {
        bool IsSatisfied(T obj);
    }
}