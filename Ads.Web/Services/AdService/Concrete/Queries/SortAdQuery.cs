using Ads.Web.Entities;

namespace Ads.Web.Services.AdService.Concrete.Queries
{
    public static class SortAdQuery
    {
        public static IQueryable<Ad> SortAdBy(this IQueryable<Ad> query, string? propertyName, bool asc)
        {
            switch (propertyName)
            {
                case "created":
                    query = asc
                        ? query.OrderBy(a => a.DateCreatedUtc)
                        : query.OrderByDescending(a => a.DateCreatedUtc);
                    break;
                case "price":
                    query = asc
                        ? query.OrderBy(a => a.Price)
                        : query.OrderByDescending(a => a.Price);
                    break;
                default:
                    query = query.OrderBy(a => a.AdId);
                    break;
            }
            return query;
        }
    }
}
