using System.Linq.Expressions;

namespace Chat.DataAccess.Specifications
{
    public class Specification<T> : ISpecification<T>
    {
        public Specification(Expression<Func<T, bool>>? criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>>? Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } =
                                                           new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>>? OrderByExpression { get; private set; }

        public Expression<Func<T, object>>? OrderByDescendingExpression { get; private set; }

        public int Take { get; private set; }   
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; } = false;
        public bool WithDistinction { get; private set; } = false;
        public bool IsChangesTrackingEnabled { get; private set; } = false;
        public IEqualityComparer<T>? DistinctionComparer { get; private set; }  = null;
        protected virtual void AddInclude(Expression<Func<T, object>> include)
        {
            Includes!.Add(include);
        }

        protected virtual void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        } 

        protected virtual void AddOrderBy(
            Expression<Func<T, object>> orderBy) =>
            OrderByExpression = orderBy;

        protected virtual void AddOrderByDescending(
            Expression<Func<T, object>> orderByDescending) =>
            OrderByDescendingExpression = orderByDescending;

        protected virtual void AddDistinction(IEqualityComparer<T>? comparer) =>
            DistinctionComparer = comparer;

        protected virtual void AsTracking()
            => IsChangesTrackingEnabled = true;
    }
}
