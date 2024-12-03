using AccountService.Data.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace AccountService.Data.Repositories
{
    public class PlayerRepository
    {
        private readonly IMongoCollection<Player> _playerCollection;
        public PlayerRepository()
        {
            var client = new MongoClient("mongodb://localhost:/");
            var database = client.GetDatabase("AccountStructureDb");
            _playerCollection = database.GetCollection<Player>("players");
        }
        public async Task<Player> Create(Player player)
        {
            await _playerCollection.InsertOneAsync(player);
            return player;
        }
        public async Task<List<Player>> GetAll()
        {
            var filter = Builders<Player>.Filter.Empty;
            var result=await _playerCollection.Find(filter).ToListAsync();
            return result;
        }
        public async Task<Player?> GetById(string id)
        {
            var filter = Builders<Player>.Filter.Eq(x=>x.Id,id);
            var result = await _playerCollection.Find(filter).FirstOrDefaultAsync();
            return result;
        }
        public async Task Update(Player updatedPlayer)
        {
            var filter = Builders<Player>.Filter.Eq(x => x.Id, updatedPlayer.Id);
            await _playerCollection.FindOneAndReplaceAsync(filter,updatedPlayer);
        }
        public async Task Remove(string id)
        {
            var filter = Builders<Player>.Filter.Eq(x => x.Id, id);
            await _playerCollection.DeleteOneAsync(filter);
        }
    }
}
