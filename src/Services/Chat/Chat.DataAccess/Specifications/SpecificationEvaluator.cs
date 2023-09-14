using Microsoft.EntityFrameworkCore;

namespace Chat.DataAccess.Specifications
{
    public class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery,
            ISpecification<T> specification)
        {
            var queryable = inputQuery;

            if (specification.Criteria is not null)
                queryable = queryable.Where(specification.Criteria);

            queryable = specification.Includes.Aggregate(
                queryable,
                (current, includeExpression) =>
                    current.Include(includeExpression));

            if (specification.OrderByExpression is not null)
                queryable = queryable.OrderBy(specification.OrderByExpression);

            else if (specification.OrderByDescendingExpression is not null)
                queryable = queryable.OrderByDescending(
                    specification.OrderByDescendingExpression);

            if (specification.IsPagingEnabled)
            {
                queryable = queryable.Skip(specification.Skip);
                queryable = queryable.Take(specification.Take);
            }

            if (!specification.IsChangesTrackingEnabled)
                queryable = queryable.AsNoTracking();

            return queryable;
        }
    }
}
