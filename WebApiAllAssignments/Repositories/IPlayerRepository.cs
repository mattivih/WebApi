using System.Threading.Tasks;
using System;
using WebApiAllAssignments.Models;

namespace WebApiAllAssignments.Repositories
{
    public interface IPlayerRepository
    {
        Task<Player> CreatePlayer(Player newPlayer);
        
        Task<Player> GetPlayer(Guid playerId);
        
        Task<Player[]> GetAllPlayers();
        
        Task<Player> ModifyPlayer(Guid playerId, Player modifiedPlayer);
        
        Task<Player> DeletePlayer(Guid playerId);
        
        Task<Item> CreateItem(Guid playerId, Item item);

        Task<Item> GetItem(Guid playerId, Guid itemId);

        Task<Item> ModifyItem(Guid playerId, Item modifiedItem, Guid itemId);
        
        Task<Item> DeleteItem(Guid playerId, Guid itemId);
    }
}