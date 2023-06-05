using Microsoft.EntityFrameworkCore;
using SecretSharing.Core.Specifications;

namespace SecretSharing.Infrastructure.Data
{
    public class SpecificationEvaluateOr<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            var Query = inputQuery.AsQueryable();
            if (spec.Criteria != null)
            {
                Query = Query.Where(spec.Criteria);
            }

            Query = spec.Includes.Aggregate(Query, (current, include) => current.Include(include));
            return Query;
        }
    }
}
