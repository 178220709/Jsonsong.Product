using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jsonsong.Dal.Common.MongoDb;
using Jsonsong.Dal.Common.MongoDB;

namespace Jsonsong.Model.Mall
{
    public class Product : BaseMongoEntity
    {
        public string Name { get; set; }

        public double SalesPrice { get; set; }

        public double OriginalPrice { get; set; }
    }
}