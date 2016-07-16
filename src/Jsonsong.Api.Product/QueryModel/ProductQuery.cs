using System.Linq;
using Jsonsong.Dal.Common.MongoDb;
using Jsonsong.Dal.Mall.Mall;

namespace Jsonsong.Api.Product.QueryModel
{
    public class ProductQuery : Pager, IQueryCollection<Dal.Mall.Mall.Product>
    {
        public double PriceMin { get; set; }

        public double PriceMax { get; set; }

        public bool? Valid { get; set; }


        public IQueryable<Dal.Mall.Mall.Product> Query(IQueryable<Dal.Mall.Mall.Product> queryable)
        {
            if (this.PriceMin > 0.01)
            {
                queryable = queryable.Where(a => a.SalesPrice > this.PriceMin);
            }
            if (this.PriceMax > 0.01)
            {
                queryable = queryable.Where(a => a.SalesPrice < this.PriceMax);
            }
            if (this.Valid.HasValue)
            {
                queryable = queryable.Where(a => a.Valid == this.Valid.Value);
            }

            return queryable.PagingQueryable(this);
        }
    }
}