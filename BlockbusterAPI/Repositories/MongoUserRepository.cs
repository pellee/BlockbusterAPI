using BlockbusterAPI.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockbusterAPI.Repositories
{
    public class MongoUserRepository : IUserRepository
    {
        private string dbName = "blockbuster";
        private string collectionName = "user";
        private readonly IMongoCollection<User> userCollection;

        public MongoUserRepository(IMongoClient mongoClient)
        {
            IMongoDatabase db = mongoClient.GetDatabase(dbName);
            userCollection = db.GetCollection<User>(collectionName);
        }
        public async Task CreateUserAsync(User user)
        {
            await userCollection.InsertOneAsync(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await userCollection.DeleteOneAsync(user => user.Id == id);
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await userCollection.Find(user => user.Id == id).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await userCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            await userCollection.ReplaceOneAsync(searcheadUser => searcheadUser.Id == user.Id, user);
        }
    }
}
