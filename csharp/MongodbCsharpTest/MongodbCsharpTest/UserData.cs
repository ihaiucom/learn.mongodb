using System;
using MongoDB.Bson.Serialization.Attributes;

namespace  ET
{
    public class UserData
    {
        [BsonId]
        public long UserId;
        public string UserName;
        public string Password;
        public string Email;
        public string Phone;
        public string Address;
        public string City;
        public string Country;
        public bool IsActive;
        public bool IsDelete;
        public UserState State;
        public DateTime RegisterTime;
        public DateTime LastLoginTime;
        public int LoginCount;
    }
}
