using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jsonsong.Dal.Common.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Jsonsong.Dal.Common.MongoDB
{
    public class Dao<TEntity> : IDisposable where TEntity : BaseEntity
    {
        public Dao() : this("") { }

        public Dao(string connectString) : this(connectString, "") { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectString">该值空的时候将取Key为MongoDb的连接串</param>
        /// <param name="collectionName">对应的collectionName</param>
        public Dao(string connectString, string collectionName)
        {
            if (string.IsNullOrEmpty(collectionName))
            {
                collectionName = typeof(TEntity).Name.ToLower();
            }

            DataBase = DataBaseManager.GetDatabase(connectString);
            Collection = DataBase.GetCollection<TEntity>(collectionName.ToLower());
        }

        public IMongoCollection<TEntity> Collection { get; private set; }

        protected IMongoDatabase DataBase { get; private set; }

        #region Sync Methods

        public IMongoQueryable<TEntity> CreateQuery()
        {
            return Collection.AsQueryable();
        }

        public bool Update(TEntity entity)
        {
            ReplaceOneAsync(entity).Wait();
            return true;
        }

        public DeleteResult Delete(string id)
        {
            var objId = ObjectId.Parse(id);
            return Collection.DeleteOne(a => a.Id == objId);
        }

        public TEntity FindOne(string id)
        {
            var objId = ObjectId.Parse(id);
            return CreateQuery().FirstOrDefault(a => a.Id == objId);
        }

        #endregion

        #region Async Methods

        public async Task InsertOneAsync(TEntity entity)
        {
            await Collection.InsertOneAsync(entity).ConfigureAwait(false);
        }

        public async Task InsertManyAsync(IEnumerable<TEntity> entities)
        {
            await Collection.InsertManyAsync(entities).ConfigureAwait(false);
        }

        /// <summary>
        /// 大批量的数据分批插入
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="batchSize"></param>
        public void InsertMany(IEnumerable<TEntity> entities, int batchSize)
        {
            var batch = new List<TEntity>();
            foreach (var entity in entities)
            {
                batch.Add(entity);
                if (batch.Count == batchSize)
                {
                    Collection.InsertManyAsync(batch).Wait();
                    batch.Clear();
                }
            }

            if (batch.Count > 0)
            {
                Collection.InsertManyAsync(batch).Wait();
                batch.Clear();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="batchSize"></param>
        public void InsertManyAsync(IEnumerable<TEntity> entities, int batchSize)
        {
            var batch = new List<TEntity>();
            foreach (var entity in entities)
            {
                batch.Add(entity);
                if (batch.Count != batchSize) continue;
                Collection.InsertManyAsync(batch);
                batch.Clear();
            }

            if (batch.Count <= 0) return;
            Collection.InsertManyAsync(batch);
            batch.Clear();
        }

        public async Task ReplaceOneAsync(TEntity entity)
        {
            await Collection.ReplaceOneAsync(a => a.Id == entity.Id, entity).ConfigureAwait(false);
        }

        public async Task DeleteOneAsync(ObjectId id)
        {
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", id);

            await Collection.FindOneAndDeleteAsync(filter).ConfigureAwait(false);
        }

        public async Task DeleteManyAsync(Expression<Func<TEntity, bool>> filter)
        {
            await Collection.DeleteManyAsync(filter).ConfigureAwait(false);
        }

        public async Task<TEntity> FindOneAsync(string id)
        {
            ObjectId objId = ObjectId.Parse(id);
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", objId);

            return await Collection.Find(filter).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Collection.Find(filter).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Collection.Find(filter).ToListAsync().ConfigureAwait(false);
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Collection.CountAsync(filter).ConfigureAwait(false);
        }

        public IMongoQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter)
        {
            return Collection.AsQueryable().Where(filter);
        }

        #endregion

      

        #region Dispose

        public void Dispose()
        {
        }

        #endregion
    }
}