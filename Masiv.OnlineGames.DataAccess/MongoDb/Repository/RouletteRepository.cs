using System;
using Masiv.OnlineGames.DataAccess.MongoDb.Base;
using Masiv.OnlineGames.DataAccess.MongoDb.Configuration;
using Masiv.OnlineGames.DataAccess.MongoDb.Interfaces;
using Masiv.OnlineGames.DataAccess.MongoDb.Models;

namespace Masiv.OnlineGames.DataAccess.MongoDb.Repository
{
    public class RouletteRepository : MongoDbRepository<Roulette>, IRouletteRepository
    {
        public RouletteRepository(IStoreDataBaseSettings settings) : base(settings)
        {
        }
    }
}
