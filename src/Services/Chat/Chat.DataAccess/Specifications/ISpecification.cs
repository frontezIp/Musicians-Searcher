using System.Linq.Expressions;

namespace Chat.DataAccess.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }

        List<Expression<Func<T, object>>> Includes { get; }

        Expression<Func<T, object>> OrderByExpression { get; }

        Expression<Func<T, object>> OrderByDescendingExpression { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }

        bool IsChangesTrackingEnabled { get; }
    }
}
