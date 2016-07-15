using Jsonsong.Dal.Common.MongoDB;

namespace Jsonsong.Dal.Mall.Mall
{
    public class Product : BaseMongoEntity
    {
        public string Name { get; set; }

        public double SalesPrice { get; set; }

        public double OriginalPrice { get; set; }
    }
}