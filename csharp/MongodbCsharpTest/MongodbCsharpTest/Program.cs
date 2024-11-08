using System;
using System.Threading;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace ET
{
    public static class Program
    {
        public static async Task Main()
        {
           
           // var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
           // ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);
           
           //1.连接字符串（mongodb数据库默认端口为：27017）
           string connStr = "mongodb://zf:123456@192.168.0.125:27017/game";
           
           
           MongoHelp.I.Init(connStr, "game");
           Console.WriteLine("--------------初始化前UserData索引");
           MongoHelp.I.PrintIndex<UserData>();
           UserHelper.InitIndexes();
           Console.WriteLine("--------------初始化完UserData索引");
           MongoHelp.I.PrintIndex<UserData>();
           Console.WriteLine("--------------------------------------");
           
           // UserHelper.Register("zf", "123456", "zf@qq.com", "111");
           // UserHelper.Register("zf2", "123456", "zf2@qq.com", "222");
           // Test.InsertOnce();
           // await Test.InsertOnceAsync();
           // Test.DeleteOne();
           // Test.DeleteOne2();
           // await Test.DeleteOneAsync();
           // await Test.DeleteManyAsync();
           // await Test.Insert10();
           
           // await Test.DeleteManyAsync2();
           // await Test.InsertManyAsync();
           // await Test.QueryAsync();
           // await Test.UpdateOneAsync();
           // await Test.UpdateManyAsync();
           // await Test.TaskReplaceOneAsync();
           // MongoHelp.I.PrintIndex<UserData>();
           MongoHelp.I.PrintIndex<UserData>("users");
           Console.WriteLine("执行完毕");

           //  //2.创建连接MongoClient实例实际上代表了一个到数据库的连接池，即使有多个线程，也只需要一个MongoClient类的实例
           // var client = new MongoClient(connStr);
           //
           // //3.使用client的GetDatabase方法获取数据库，即使该数据库不存在，也会自动创建；
           // IMongoDatabase db = client.GetDatabase("game");
           
           
           
           
//            
//            //4.获取数据集 collection；BsonDocument是在数据没有预先定义好的情况下使用的。
//            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("runoob1");
//
// //5.插入一条数据;
//            var document = new BsonDocument { { "id", 2 }, { "name", "aa" } };
//            collection.InsertOne(document);
//
//
// //6.查询数据1
//            var res = collection.Find(new BsonDocument()).ToList(); //查询整个数据集
//            foreach (var item in res)
//            {
//                Console.WriteLine(item);
//            }
// //6.查询数据2
//            var res_a = collection.Find(new BsonDocument()).FirstOrDefault(); //查询当前数据集的第一条数据，没有则返回null
//            Console.WriteLine(res_a);
//
//
// //6.升序降序查询3：
//            var sort_ascending = Builders<BsonDocument>.Sort.Ascending("id"); //根据id升序
//            var sort_descending = Builders<BsonDocument>.Sort.Descending("id"); //根据id降序
//            var res_c = collection.Find(Builders<BsonDocument>.Filter.Lt("id", 10) & Builders<BsonDocument>.Filter.Gte("id", 2)).Limit(50).Sort(sort_ascending).ToCursor(); //查询id小于10，大于2的数据
//            foreach (var item in res_c.ToEnumerable())
//            {
//                Console.WriteLine(item);
//            }
//
// //7.更新数据，更新支持添加新的field, 如:
//            collection.UpdateMany(Builders<BsonDocument>.Filter.Eq("id", 2), Builders<BsonDocument>.Update.Set("name", "hello"));   //将id字段为2的名字都改为“hello”;
//
// //8.删除
//            // collection.DeleteMany(Builders<BsonDocument>.Filter.Eq("id", 2));
            
            // while (true)
            // {
            //     Thread.Sleep(1);
            //     try
            //     {
            //        
            //     }
            //     catch (Exception e)
            //     {
            //         Console.WriteLine(e);
            //     }
            // }
        }
    }
}