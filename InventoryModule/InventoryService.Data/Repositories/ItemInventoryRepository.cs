using InventoryService.Data.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Data.Repositories
{
    public class ItemInventoryRepository
    {
        private readonly IMongoCollection<ItemInventory> _itemInventoryCollection;
        public ItemInventoryRepository()
        {
            var client = new MongoClient("mongodb://localhost:/");
            var database = client.GetDatabase("InventoryDb");
            _itemInventoryCollection = database.GetCollection<ItemInventory>("iteminventories");
        }
        public async Task<List<ItemInventory>?> GetAll()
        {
            var filter = Builders<ItemInventory>.Filter.Empty;
            var result = await _itemInventoryCollection.Find(filter).ToListAsync();
            return result;
        }
        public async Task<ItemInventory> Create(ItemInventory inventory)
        {
            await _itemInventoryCollection.InsertOneAsync(inventory);
            return inventory;
        }
        public async Task<ItemInventory?> GetItemInventory(string itemId,string inventoryId)
        {
            var filterI = Builders<ItemInventory>.Filter.Eq(x => x.ItemId, itemId);
            var filterII = Builders<ItemInventory>.Filter.Eq(x => x.InventoryId, inventoryId);
           var filter= Builders<ItemInventory>.Filter.And(filterI, filterII);
            var result = await _itemInventoryCollection.Find(filter).FirstOrDefaultAsync();
            return result;
        }
        public async Task Update(ItemInventory updatedItemInventory)
        {
            var filter = Builders<ItemInventory>.Filter.Eq(x => x.Id, updatedItemInventory.Id);
            await _itemInventoryCollection.FindOneAndReplaceAsync(filter, updatedItemInventory);
        }
        public async Task Remove(string id)
        {
            var filter = Builders<ItemInventory>.Filter.Eq(x => x.Id, id);
            await _itemInventoryCollection.DeleteOneAsync(filter);
        }
    }
}
