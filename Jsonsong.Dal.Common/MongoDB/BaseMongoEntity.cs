using System;
using Jsonsong.Dal.Common.MongoDb;
using MongoDB.Bson.Serialization.Attributes;

namespace Jsonsong.Dal.Common.MongoDB
{
    [BsonIgnoreExtraElements]
    public class BaseMongoEntity : BaseEntity
    {
        public DateTime CreateTime { get; set; }

        public DateTime LastModifyTime { get; set; }

        public bool Valid { get; set; }

    }
}
