using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Jsonsong.Api.Products.Controllers.Base;
using Jsonsong.Dal.Mall.Mall;
using Microsoft.AspNetCore.Mvc;

namespace Jsonsong.Api.Products.Controllers
{
    /// <summary>
    /// ProductsController
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductsController : BaseCrudController<Product>
    {
        /// <summary>
        /// Creates a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("p2")]
        public int Create([FromBody, Required]Product product)
        {
            return 1;
        }
    }
}
