using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Masiv.OnlineGames.Business.Interfaces;
using Masiv.OnlineGames.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Masiv.OnlineGames.RouletteApi.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class RouletteController : Controller
    {
        private readonly IRouletteBll rouletteBll;
        public RouletteController(IRouletteBll _rouletteBll)
        {
            this.rouletteBll = _rouletteBll; 
        }
      
        [HttpGet]
        public async Task<IActionResult> GetAllRoulette()
        {
            var result = await rouletteBll.GetRouletteAsync();

            return Ok(result);
        }

       
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("new")]
        public async Task<IActionResult> NewRoulette()
        {
            var result = await rouletteBll.InsertRouletteAsync();

            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id ruolette</param>
        /// <returns></returns>
        [HttpPut("{id}/open")]
        public async Task<IActionResult> OpenRoulette(string id)
        {
            var result = await rouletteBll.UpdateRouletteAsync(id);

            return Ok(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bet.amount">It lets make a bet between [0.5 and 10000, red or black]</param>
        /// <param name="bet.value">[0,36] number [37=> red, 38=> black] </param>
        /// <header="authentication">value = true -> is autheticate</param>
        /// <returns></returns>
        [HttpPost("bet")]
        public async Task<IActionResult> NewBet([FromBody] Bet bet)
        {
            string token = Request.Headers["authentication"];
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            if (ModelState.IsValid && !string.IsNullOrEmpty(bet.id))
            {
                var result = await rouletteBll.InsertBetAsync(bet);

                return Ok(result);           
            }
            else
            {

                return BadRequest(new
                {
                    error = "Por favor validar la información",
                    result = string.Empty
                });
            }        
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id ruolette</param>
        /// <returns></returns>
        [HttpPost("{id}/close")]
        public async Task<IActionResult> CloseBets(string id)
        {
            var result = await rouletteBll.UpdateBetAsync(id);

            return Ok(result);
        }
    }
}
