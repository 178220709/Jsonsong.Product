using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Jsonsong.Api.Product.Controllers.Base;
using Jsonsong.Api.Product.QueryModel;
using Jsonsong.Dal.Common.MongoDb;
using Jsonsong.Dal.Mall.Mall;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Jsonsong.Api.Product.Controllers
{
    /// <summary>
    /// ProductsController
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductsController : BaseCrudController<Dal.Mall.Mall.Product>
    {
        /// <summary>
        /// Creates a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("test")]
        public string Test([FromBody, Required] Dal.Mall.Mall.Product product)
        {
            return product == null ? "null" : JsonConvert.SerializeObject(product);
        }
        /// <summary>
        /// Query with ProductQuery model
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("query")]
        public IEnumerable<Dal.Mall.Mall.Product> Query([FromQuery, Required] ProductQuery query)
        {
            return (query ?? new ProductQuery()).Query(dao.CreateQuery());
        }


        [HttpGet("test1")]
        public string Test1()
        {
            return "path:"+Startup.Tmp1;
        }
    }
}