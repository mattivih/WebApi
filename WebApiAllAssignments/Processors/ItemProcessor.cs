using System;
using System.Threading.Tasks;
using WebApiAllAssignments.Models;
using WebApiAllAssignments.Repositories;

namespace WebApiAllAssignments.Processors
{
    public class ItemProcessor
    {
        private readonly IPlayerRepository _repository;
        
        public ItemProcessor(IPlayerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Item> CreateItem(Guid playerId, NewItem newItem)
        {
            Random r = new Random();
            int random = r.Next(1, 1000);

            Item item = new Item();
            item.Name = newItem.Name;
            item.Level = newItem.Level;
            item.Type = newItem.Type;
            item.Id = Guid.NewGuid();
            item.Price = random;
            item.CreationDate = DateTime.Now;
            return await _repository.CreateItem(playerId, item);
        }
        
        public Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            return _repository.GetItem(playerId, itemId);
        }

        public async Task<Item> ModifyItem(Guid playerId, ModifiedItem modifiedItem, Guid itemId)
        {
            Item item = new Item();
            item = await _repository.GetItem(playerId, itemId);
            item.Name = modifiedItem.Name;
            item.Level = modifiedItem.Level;
            await _repository.ModifyItem(playerId, item, itemId);
            return await _repository.ModifyItem(playerId, item, itemId);
        }

        public Task<Item> DeleteItem(Guid playerId, Guid itemId)
        {
            return _repository.DeleteItem(playerId, itemId);
        }
    }
}