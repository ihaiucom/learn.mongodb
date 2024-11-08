using MongoDB.Driver;

namespace ET
{
    
    public static class UserHelper
    {
        public static string CollectionName = "users";
        public static UserData Register(string userName, string password, string email, string phone)
        {
            UserData userData = new UserData();
            userData.UserId = MongoHelp.I.GetNextSequence<UserData>();
            userData.UserName = userName;
            userData.Password = password;
            userData.Email = email;
            userData.Phone = phone;
            userData.RegisterTime = DateTime.Now;
            
            MongoHelp.I.InsertOnce(userData);
            
            return userData;
        }
        
        public static UserData Login(UserLoginType loginType, string account, string password)
        {
            return null;
        }

        public static UserData GetUserData(long userId)
        {
            
            return null;
        }
        
        public static List<UserData> GetUserDataList(int pageIndex, int pageSize)
        {
            
            return null;
        }


    }
}
