using System.Collections.Generic;
using System.Linq;
using Jsonsong.Dal.Common.MongoDb;
using Jsonsong.Dal.Common.MongoDB;
using Microsoft.AspNetCore.Mvc;

namespace Jsonsong.Api.Product.Controllers.Base
{
    /// <summary>
    /// rest基类 默认暴露基础的http 谓词
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseCrudController<T> : Controller where T : BaseEntity
    {
        protected Dao<T> dao = new Dao<T>();

        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual IEnumerable<T> Get(Pager pager)
        {
            return dao.CreateQuery().Skip(pager.Skip).Take(pager.PageSize).ToList();
        }

        /// <summary>
        /// GetById id is mongo objectId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public virtual T Get(string id)
        {
            return dao.FindOne(id);
        }

        /// <summary>
        /// Post a T to insert
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public virtual void Post([FromBody] T value)
        {
            dao.InsertOneAsync(value).Wait();
        }


        /// <summary>
        ///  Updates all properties of a specific T
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public virtual void Put(int id, [FromBody] T value)
        {
            dao.Update(value);
        }

        /// <summary>
        /// DELETE by id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public virtual void Delete(string id)
        {
            dao.Delete(id);
        }
    }
}