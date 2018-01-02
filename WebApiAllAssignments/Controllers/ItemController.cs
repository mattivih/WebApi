using System;
using System.Threading.Tasks;
using WebApiAllAssignments.Models;
using WebApiAllAssignments.Processors;
using WebApiAllAssignments.Validation;
using Microsoft.AspNetCore.Mvc;

namespace WebApiAllAssignments.Controllers
{
    [Route("api/players/{playerId}/items")]
    public class ItemController
    {
        private ItemProcessor _processor;    

        public ItemController(ItemProcessor processor)
        {
            _processor = processor;
        }

        [HttpPost]
        [ValidateModel]
        public Task<Item> CreateItem(Guid playerId, [FromBody]NewItem newItem)
        {
            return _processor.CreateItem(playerId, newItem);
        }

        [HttpGet("{itemId}")]
        public Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            return _processor.GetItem(playerId, itemId);
        }

        [HttpPut("{itemId}")]
        [ValidateModel]
        public Task<Item> ModifyItem(Guid playerId, [FromBody] ModifiedItem modifiedItem, Guid itemId)
        {
            return _processor.ModifyItem(playerId, modifiedItem, itemId);
        }

        [HttpDelete("{itemId}")]
        public Task<Item> DeleteItem(Guid playerId, Guid itemId)
        {
            return _processor.DeleteItem(playerId, itemId);
        }
    }
}