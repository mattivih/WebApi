using System;
using System.Collections.Generic;
using WebApiAllAssignments.Exceptions;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebApiAllAssignments.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using WebApiAllAssignments.Mongodb;
using System.Linq;

namespace WebApiAllAssignments.Repositories
{
    public class MongoDbRepository : IPlayerRepository
    {
        private IMongoCollection<Player> _collection;

        public MongoDbRepository(MongoDbClient client)
        {
            // Using database named "game"
            IMongoDatabase database = client.GetDatabase("game");
            
            // Getting collection name "game"
            _collection = database.GetCollection<Player>("players");

        }
            
        public async Task<Player> CreatePlayer(Player newPlayer)
        {
            _collection.InsertOne(newPlayer);
            return newPlayer;
        }

        public async Task<Player> GetPlayer(Guid playerId)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var cursor = await _collection.FindAsync(filter);
            Player player = await cursor.FirstOrDefaultAsync();
            if(player == null)
                throw new NotFoundException();
            return player;
        }

        public async Task<Player[]> GetAllPlayers()
        {
            var filter = FilterDefinition<Player>.Empty;
            var cursor = await _collection.FindAsync(filter);
            var players = await cursor.ToListAsync();
            return players.ToArray();
        }

        public async Task<Player> ModifyPlayer(Guid playerId, Player modifiedPlayer)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            
            // Returns the actual result (modified player)
            var options = new FindOneAndReplaceOptions<Player, Player>()
            {
                ReturnDocument = ReturnDocument.After
            };
            
            var player = await _collection.FindOneAndReplaceAsync(filter, modifiedPlayer, options);
            return player;
        }

        public async Task<Player> DeletePlayer(Guid playerId)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var cursor = await _collection.FindOneAndDeleteAsync(filter);
            return cursor;
        }

        public async Task<Item> CreateItem(Guid playerId, Item item)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            var player = await GetPlayer(playerId);
            if (player.Level <= item.Level && item.Type == "Sword")
                throw new LevelTooLowException();
            player.Items.Add(item);
            await ModifyPlayer(playerId, player);
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
            Player player = await GetPlayer(playerId);
            int indexNo = 0;
            foreach (Item i in player.Items)
            {
                if (i.Id.Equals(itemId))
                {
                    player.Items[indexNo] = modifiedItem;
                    await ModifyPlayer(playerId, player);
                    return player.Items[indexNo];
                }
                indexNo++;
            }
            throw new ItemNotFoundException();
        }

        public async Task<Item> DeleteItem(Guid playerId, Guid itemId)
        {
            Player player = await GetPlayer(playerId);
            foreach (Item i in player.Items)
            {
                if (i.Id.Equals(itemId))
                {
                    player.Items.Remove(i);

                    await _collection.ReplaceOneAsync(Builders<Player>.Filter.Eq(p => p.Id, playerId), player);
                    return i;
                }
            }
            throw new ItemNotFoundException();
        }
    }
}