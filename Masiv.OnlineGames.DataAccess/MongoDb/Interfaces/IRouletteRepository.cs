using System;
using Masiv.OnlineGames.DataAccess.MongoDb.Base;
using Masiv.OnlineGames.DataAccess.MongoDb.Models;

namespace Masiv.OnlineGames.DataAccess.MongoDb.Interfaces
{
    public interface IRouletteRepository : IMongoDbRepository<Roulette>
    {

    }
}
