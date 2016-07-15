using MongoDB.Bson;

namespace Jsonsong.Dal.Common.MongoDb
{
    /// <summary>
    /// Mongo Db Base Entity
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual ObjectId Id { get; set; }
    }
}