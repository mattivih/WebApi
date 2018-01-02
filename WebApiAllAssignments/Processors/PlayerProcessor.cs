using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiAllAssignments.Models;
using WebApiAllAssignments.Repositories;

namespace WebApiAllAssignments.Processors
{
    public class PlayerProcessor
    {
        private readonly IPlayerRepository _repository;
        
        public PlayerProcessor(IPlayerRepository repository)
        {
            _repository = repository;
        }

        public Task<Player> CreatePlayer(NewPlayer newPlayer)
        {
            Player player = new Player();//(Guid.NewGuid(), newPlayer.Name);
            List<Item> newList = new List<Item>();
            player.Id = Guid.NewGuid();
            player.Name = newPlayer.Name;
            player.Items = newList;
            player.Level = newPlayer.Level;
            return _repository.CreatePlayer(player);
        }
        
        public async Task<Player> GetPlayer(Guid playerId)
        {
            return await _repository.GetPlayer(playerId);
        }

        
        public Task<Player[]>GetAllPlayers()
        {
            return _repository.GetAllPlayers();
        }
   
        public async Task<Player> ModifyPlayer(Guid playerId, ModifiedPlayer modifiedPlayer)
        {
            Player player = await _repository.GetPlayer(playerId);
            player.Name = modifiedPlayer.Name;
            player.Level = modifiedPlayer.Level;
            return await _repository.ModifyPlayer(playerId, player);
        }
        
        public Task<Player> DeletePlayer(Guid playerId)
        {
            return _repository.DeletePlayer(playerId);
        }
        
    }
}