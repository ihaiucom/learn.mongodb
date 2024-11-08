using System.Linq.Expressions;
using MongoDB.Driver;

namespace ET
{
    
    public class MongoHelp
    {
        private static MongoHelp _I;
        public static MongoHelp I
        {
            get
            {
                if (_I == null)
                {
                    _I = new MongoHelp();
                }
                return _I;
            }
        }

        public string Uri;
        public string DbName;
        public MongoClient mongoClient;
        public IMongoDatabase database;
        public void Init(string uri, string dbName)
        {
            Uri = uri;
            DbName = dbName;
            //1.创建MongoClient实例，传入连接字符串
            
            //2.创建连接MongoClient实例实际上代表了一个到数据库的连接池，即使有多个线程，也只需要一个MongoClient类的实例
            mongoClient = new MongoClient(uri);

            //3.使用client的GetDatabase方法获取数据库，即使该数据库不存在，也会自动创建；
            database = mongoClient.GetDatabase("game");
        }
        
        public IMongoCollection<T> GetCollection<T>(string collection = null)
        {
            return database.GetCollection<T>(collection ?? typeof (T).FullName);
        }

        public void InsertOnce<T>(T data, string collection = null)
        {
            if (collection == null)
            {
                collection = typeof (T).FullName;
            }
            
            GetCollection<T>(collection).InsertOne(data);
        }

        public  async Task  InsertOneAsync<T>(T data, string collection = null)
        {
            if (collection == null)
            {
                collection = typeof (T).FullName;
            }

            
            await GetCollection<T>(collection).InsertOneAsync(data);
        }

        public async Task InsertManyAsync<T>(List<T> data, string collection = null)
        {
            if (collection == null)
            {
                collection = typeof (T).FullName;
            }
            
            await GetCollection<T>(collection).InsertManyAsync(data);
        }

        public DeleteResult DeleteOne<T>(FilterDefinition<T> filter, string collection = null)
        { 
            if (collection == null)
            {
                collection = typeof (T).FullName;
            }
            return GetCollection<T>(collection).DeleteOne(filter);
        }
        public DeleteResult DeleteOne2<T>(Expression<Func<T, bool>> filter, string collection = null)
        { 
            if (collection == null)
            {
                collection = typeof (T).FullName;
            }
            return GetCollection<T>(collection).DeleteOne(filter);
        }
        
        public async Task<DeleteResult> DeleteOneAsync<T>(Expression<Func<T, bool>> filter, string collection = null)
        { 
            if (collection == null)
            {
                collection = typeof (T).FullName;
            }
            return await GetCollection<T>(collection).DeleteOneAsync(filter);
        }

        public async Task<DeleteResult> DeleteManyAsync<T>(Expression<Func<T, bool>> filter, string collection = null)
        {
            if (collection == null)
            {
                collection = typeof (T).FullName;
            }
            return await GetCollection<T>(collection).DeleteManyAsync(filter);
        }
        
        public async Task<T> QueryOneAsync<T>(Expression<Func<T, bool>> filter, string collection = null)
        {
            if (collection == null)
            {
                collection = typeof (T).FullName;
            }
            IAsyncCursor<T> cursor = await GetCollection<T>(collection).FindAsync(filter);
            return await cursor.FirstOrDefaultAsync();
        }

        public async Task<List<T>> QueryAsync<T>(Expression<Func<T, bool>> filter, string collection = null)
        {
            if (collection == null)
            {
                collection = typeof (T).FullName;
            }
            IAsyncCursor<T> cursor = await GetCollection<T>(collection).FindAsync(filter);
            return await cursor.ToListAsync();
        }
        
          
        public async  Task<List<T>> QueryJsonAsync<T>(string json, string collection = null)
        {
            if (collection == null)
            {
                collection = typeof (T).FullName;
            }
            FilterDefinition<T> filterDefinition = new JsonFilterDefinition<T>(json);
            IAsyncCursor<T> cursor = await GetCollection<T>(collection).FindAsync(filterDefinition);
            return await cursor.ToListAsync();
        }
        
        public async Task<UpdateResult>  UpdateOneAsync<T>(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, string collection = null)
        {
            if (collection == null)
            {
                collection = typeof (T).FullName;
            }
            return await GetCollection<T>(collection).UpdateOneAsync(filter, update);
        }
        
        public async Task<UpdateResult>  UpdateManyAsync<T>(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, string collection = null)
        {
            if (collection == null)
            {
                collection = typeof (T).FullName;
            }
            return await GetCollection<T>(collection).UpdateManyAsync(filter, update);
        }
        public async Task<ReplaceOneResult> ReplaceOneAsync<T>(Expression<Func<T, bool>> filter, T data, bool IsUpsert = true, string collection = null)
        {
            if (collection == null)
            {
                collection = typeof (T).FullName;
            }
            return await GetCollection<T>(collection).ReplaceOneAsync(filter, data, new ReplaceOptions() { IsUpsert = IsUpsert });
        }

        public async Task<long> GetNextSequenceAsync<T>(string collection = null, long start = 1)
        {
            if (collection == null)
            {
                collection = typeof (T).FullName;
            }
            
            IAsyncCursor<CollectionCounter> cursor = await GetCollection<CollectionCounter>().FindAsync(x => x.CollectionName == collection);
            var collectionCounter =  await cursor.FirstOrDefaultAsync();
            if (collectionCounter == null)
            {
                collectionCounter = new CollectionCounter() { CollectionName = collection, Count = start };
            }
            else
            {
                collectionCounter.Count++;
            }
            
            await ReplaceOneAsync<CollectionCounter>(x => x.CollectionName == collection, collectionCounter);

            return collectionCounter.Count;
        }

        public long GetNextSequence<T>(string collection = null, long start = 1)
        {
            if (collection == null)
            {
                collection = typeof (T).FullName;
            }
            var filter = Builders<CollectionCounter>.Filter.Eq(x => x.CollectionName, collection);
            var update = Builders<CollectionCounter>.Update.Inc(x => x.Count, 1);
            var option = new FindOneAndUpdateOptions<CollectionCounter, CollectionCounter>() { ReturnDocument = ReturnDocument.After };
            var collectionCounter = GetCollection<CollectionCounter>().FindOneAndUpdate(filter, update, option);
            if (collectionCounter == null)
            {
                collectionCounter = new CollectionCounter() { CollectionName = collection, Count = start };
                GetCollection<CollectionCounter>().InsertOne(collectionCounter);
            }
            Console.WriteLine($"GetNextSequence {collection}: {collectionCounter.Count}");
            return collectionCounter.Count;
        }
    }
}
