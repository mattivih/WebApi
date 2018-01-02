using System;
using System.Threading.Tasks;
using WebApiAllAssignments.Models;
using WebApiAllAssignments.Processors;
using WebApiAllAssignments.Validation;
using Microsoft.AspNetCore.Mvc;

namespace WebApiAllAssignments.Controllers
{
    [Route("api/players")]
    public class PlayerController : Controller
    {
        private PlayerProcessor _processor;

        public PlayerController(PlayerProcessor processor)
        {
            _processor = processor;
        }

        [HttpPost]
        [ValidateModel]
        public Task<Player> CreatePlayer([FromBody]NewPlayer newPlayer)
        {
            return _processor.CreatePlayer(newPlayer);
        }

        
        [HttpGet("{playerId}")]
        public async Task<Player> GetPlayer(Guid playerId)
        {
            Player player = await _processor.GetPlayer(playerId);
            return player;
        }

        [HttpGet]
        public async Task<Player[]> GetAllPlayers()
        {
            Player[] players = await _processor.GetAllPlayers();
            return players;
        }

        [HttpPut("{playerId}")]
        [ValidateModel]
        public async Task<Player> ModifyPlayer(Guid playerId, [FromBody]ModifiedPlayer modifiedPlayer)
        {
            Player _player = await _processor.ModifyPlayer(playerId, modifiedPlayer);
            return _player;
        }
        
        [HttpDelete("{playerId}")]
        public async Task<Player> DeletePlayer(Guid playerId)
        {
            Player player = await _processor.DeletePlayer(playerId);
            return player;
        }
    }
}