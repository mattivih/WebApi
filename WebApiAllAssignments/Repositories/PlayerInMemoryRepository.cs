using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;
using WebApiAllAssignments.Exceptions;
using WebApiAllAssignments.Middlewares;
using WebApiAllAssignments.Models;

namespace WebApiAllAssignments.Repositories
{
    public class PlayerInMemoryRepository : IPlayerRepository
    {
        private Dictionary<Guid, Player> _players = new Dictionary<Guid, Player>();

        public async Task<Player> CreatePlayer(Player newPlayer)
        {
            _players.Add(newPlayer.Id, newPlayer);
            return newPlayer;
        }
        
        public async Task<Player> GetPlayer(Guid playerId)
        {
            return _players[playerId];
        }
        
        public async Task<Player[]> GetAllPlayers()
        {
            return _players.Values.ToArray();
        }

        public async Task<Player> ModifyPlayer(Guid playerId, Player modifiedPlayer)
        {
            _players[playerId] = modifiedPlayer;
            return modifiedPlayer;
        }
        
        public async Task<Player> DeletePlayer(Guid playerId)
        {
            Player player = _players[playerId];
            _players.Remove(playerId);
            return player;
        }

        public async Task<Item> CreateItem(Guid playerId, Item item)
        {
            Player player = await GetPlayer(playerId);
            if (player.Level <= item.Level && item.Type == "Sword")
            {
                throw new LevelTooLowException();
            }
            else
            {
                player.Items.Add(item);
                await DeletePlayer(playerId);
                _players.Add(playerId, player);
            }
            return item;
        }
        
        public async Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            Item itemToReturn = null;
            Player player = await GetPlayer(playerId);
            foreach (Item i in player.Items)
            {
                if (i.Id.Equals(itemId))
                {
                    itemToReturn = i;
                    break;
                }
            }

            if (itemToReturn == null) {
                throw new ItemNotFoundException();
            }
            return itemToReturn;
        }

        public async Task<Item> ModifyItem(Guid playerId, Item modifiedItem, Guid itemId)
        {
            Player player = _players[playerId];
            int indexNo = 0;
            foreach (Item i in player.Items)
            {
                if (i.Id.Equals(itemId))
                    break;
                indexNo++;
            }
            player.Items[indexNo] = modifiedItem;
            _players[playerId] = player;
            return player.Items[indexNo];
        }

        public async Task<Item> DeleteItem(Guid playerId, Guid itemId)
        {
            Item item = await GetItem(playerId, itemId);
            if (item.Id == itemId)
            {
                Player player = await DeletePlayer(playerId);
                player.Items.Remove(item);
                await CreatePlayer(player);

            }
            return item;
        }
    }
}
