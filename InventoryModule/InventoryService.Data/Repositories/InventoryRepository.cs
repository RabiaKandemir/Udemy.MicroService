using InventoryService.Data.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Data.Repositories
{
    public class InventoryRepository
    {
        private readonly IMongoCollection<Inventory> _inventoryCollection;
        public InventoryRepository()
        {
            var client = new MongoClient("mongodb://localhost:/");
            var database = client.GetDatabase("InventoryDb");
            _inventoryCollection = database.GetCollection<Inventory>("inventories");
        }
        public async Task<List<Inventory>?> GetAll()
        {
            var filter = Builders<Inventory>.Filter.Empty;
            var result = await _inventoryCollection.Find(filter).ToListAsync();
            return result;
        }
        public async Task<Inventory> Create(Inventory inventory)
        {
            await _inventoryCollection.InsertOneAsync(inventory);
            return inventory;
        }
        public async Task<Inventory?> GetById(string id)
        {
            var filter = Builders<Inventory>.Filter.Eq(x => x.Id, id);
            var result = await _inventoryCollection.Find(filter).FirstOrDefaultAsync();
            return result;
        }
        public async Task Update(Inventory updatedInventory)
        {
            var filter = Builders<Inventory>.Filter.Eq(x => x.Id, updatedInventory.Id);
            await _inventoryCollection.FindOneAndReplaceAsync(filter, updatedInventory);
        }
        public async Task Remove(string id)
        {
            var filter = Builders<Inventory>.Filter.Eq(x => x.Id, id);
            await _inventoryCollection.DeleteOneAsync(filter);
        }
    }
}
