using System;
using System.IO;
using AutoMapper;
using Masiv.OnlineGames.DataAccess.MongoDb.Models;
using Masiv.OnlineGames.Models;

namespace Masiv.OnlineGames.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RouletteModel, Roulette>();
            CreateMap<BetsModel, Bets>();
        }
    }
}

