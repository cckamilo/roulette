using System;
namespace Masiv.OnlineGames.DataAccess.MongoDb.Configuration
{
    public class StoreDataBaseSettings :IStoreDataBaseSettings
    {
        public string collectionName { get; set; }
        public string connectionString { get; set; }
        public string dataBaseName { get; set; }
    }
}
