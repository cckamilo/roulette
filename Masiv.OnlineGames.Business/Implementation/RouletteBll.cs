using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Masiv.OnlineGames.Business.Interfaces;
using Masiv.OnlineGames.DataAccess.MongoDb.Interfaces;
using Masiv.OnlineGames.DataAccess.MongoDb.Models;
using Masiv.OnlineGames.Models;
using Masiv.OnlineGames.Models.Response;
using MongoDB.Bson;

namespace Masiv.OnlineGames.Business.Implementation
{
    public class RouletteBll: IRouletteBll
    {

        private ServiceResponse response;
        private readonly IRouletteRepository repository;

        public RouletteBll(IRouletteRepository _repository)
        {
            this.repository = _repository;
            
        }

        public Task<ServiceResponse> GetRouletteAsync()
        {
           throw new NotImplementedException();
        }

        public Task<ServiceResponse> GetByIdRouletteAsync(string id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponse> InsertRouletteAsync()
        {
            try
            {
                response = new ServiceResponse();
                var roullete = new Roulette
                {
                    id = ObjectId.GenerateNewId().ToString()
                };
                var result = await repository.InsertAsync(roullete);
                response.result = result;
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
            }
           
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResponse> UpdateRouletteAsync(string id)
        {
            try
            {
                response = new ServiceResponse();
                var exist = await this.repository.GetByIdAsync(id) != null ? true : false;
                if (exist)
                {
                    var roullete = new Roulette
                    {
                        id = id,
                        isOpen = true,
                        openedAt = DateTime.Now
                    };
                    var result = await this.repository.UpdateAsync(roullete);
                    response.result = result;
                 }
                else
                {
                    response.error = "No existe la ruleta";
                }
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
            }
          
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bet"></param>
        /// <returns></returns>
        public async Task<ServiceResponse> InsertBetAsync(Bet bet)
        {
            try
            {
                response = new ServiceResponse();
  
                var data = await this.repository.GetByIdAsync(bet.id);
                if (data != null && data.isOpen)
                {
                    var bets = new List<Bets>();
                    bets.Add(new Bets() { value = bet.value, amount = bet.amount });
                    if (data.bets is null)
                    {
                        data.bets = bets;
                    }
                    else
                    {
                        data.bets.AddRange(bets);
                    }                         
                    var result = await this.repository.UpdateAsync(data);
                    response.result = data;
                }
                else
                {
                    response.error = "No existe ruleta";
                }
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
            }

            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bet"></param>
        /// <returns></returns>
        public Roulette generateRouletteResult(Roulette roulette)
        {
            Random random = new Random();     
            var number = random.Next(0, 38);
            roulette.winningNumber = number;
            roulette.winningColor = number % 2 == 0 ? "Red" : "Black";
            roulette.bets.ForEach(x =>
            {
                if ((x.value.Equals(37) && roulette.winningColor == "Red") || (x.value.Equals(38) && roulette.winningColor == "Black"))
                {
                    x.payout = x.amount * 1.8;
                }
                else
                {
                    if (x.value == number)
                    {
                        x.payout = number * 5;
                    }
                }               
            });

            return roulette;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResponse> UpdateBetAsync(string id)
        {   
            try
            {
                response = new ServiceResponse();
                var data = await this.repository.GetByIdAsync(id);
                if (data != null && data.isOpen)
                {
                    var winner = generateRouletteResult(data);
                    winner.isOpen = false;
                    winner.closedAt = DateTime.Now;
                    var result = await this.repository.UpdateAsync(winner);
                    response.result = winner;
                }
                else
                {
                    response.error = "No existe ruleta";
                }            
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
            }
           

            return response;
        }
    }
}
