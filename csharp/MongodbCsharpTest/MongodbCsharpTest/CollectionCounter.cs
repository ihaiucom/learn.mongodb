using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public class CollectionCounter
    {
        [BsonId]
        public string CollectionName;
        public long Count;
    }
}
