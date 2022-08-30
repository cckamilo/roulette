using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Masiv.OnlineGames.Business.Interfaces;
using Masiv.OnlineGames.DataAccess.MongoDb.Interfaces;
using Masiv.OnlineGames.DataAccess.MongoDb.Models;
using Masiv.OnlineGames.Models;
using Masiv.OnlineGames.Models.Response;
using MongoDB.Bson;
using System.Linq;
namespace Masiv.OnlineGames.Business.Implementation
{
    public class RouletteBll: IRouletteBll
    {

        private ServiceResponse response;
        private readonly IRouletteRepository repository;
        private readonly IMapper iMapper;
        public RouletteBll(IRouletteRepository _repository, ServiceResponse response, IMapper iMapper)
        {
            this.repository = _repository;
            this.response = response;
            this.iMapper = iMapper;
        }

        public async Task<ServiceResponse> GetRouletteAsync()
        {
            try
            {

                var result = await repository.GetAllAsync();
                response.result = result;
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
            }

            return response;
        }

        public Task<ServiceResponse> GetByIdRouletteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse> InsertRouletteAsync()
        {
            try
            {        
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

        public async Task<ServiceResponse> UpdateRouletteAsync(string id)
        {
            try
            {          
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

        public async Task<ServiceResponse> InsertBetAsync(BetsModel bet)
        {
            try
            {              
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

        public Roulette CloseBetsRoulette(Roulette roulette)
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

        public async Task<ServiceResponse> UpdateBetAsync(string id)
        {   
            try
            {              
                var data = await this.repository.GetByIdAsync(id);
                if (data != null && data.isOpen)
                {

                    var items = CloseBetsRoulette(data);
                    items.isOpen = false;
                    items.closedAt = DateTime.Now;
                    var result = await this.repository.UpdateAsync(items);
                    items.bets = items.bets.OrderByDescending(x => x.payout > 0).ToList();
                    response.result = items;
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
