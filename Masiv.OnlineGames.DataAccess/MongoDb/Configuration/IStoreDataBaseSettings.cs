using System;
namespace Masiv.OnlineGames.DataAccess.MongoDb.Configuration
{
    public interface IStoreDataBaseSettings
    {
        string collectionName { get; set; }
        string connectionString { get; set; }
        string dataBaseName { get; set; }
    }
}
