using System;
using System.Threading.Tasks;
using Masiv.OnlineGames.Models;
using Masiv.OnlineGames.Models.Response;

namespace Masiv.OnlineGames.Business.Interfaces
{
    public interface IRouletteBll
    {
        Task<ServiceResponse> InsertRouletteAsync();
        Task<ServiceResponse> GetByIdRouletteAsync(string id);
        Task<ServiceResponse> GetRouletteAsync();
        Task<ServiceResponse> UpdateRouletteAsync(string id);
        Task<ServiceResponse> InsertBetAsync(Bet bet);
        Task<ServiceResponse> UpdateBetAsync(string id);
    }
}
