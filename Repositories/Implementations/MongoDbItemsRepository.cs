using System;
using System.Collections.Generic;
using Catalog.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories.Implementations
{
    public class MongoDbItemsRepository : IItemRepository
    {
        private const string DatabaseName = "CATALOG";
        private const string CollectionName = "ITEMS";
        private readonly IMongoCollection<Item> _itemsCollection;
        private readonly FilterDefinitionBuilder<Item> _filterDefinitionBuilder = Builders<Item>.Filter;

        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(DatabaseName);
            _itemsCollection = database.GetCollection<Item>(CollectionName);
        }

        public Item GetItem(Guid id)
        {
            var filter = _filterDefinitionBuilder.Eq(item => item.Id, id);
            return _itemsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return _itemsCollection.Find(new BsonDocument()).ToList();
        }

        public void CreateItem(Item item)
        { 
            _itemsCollection.InsertOne(item);
        }

        public void UpdateItem(Item item)
        {
            var filter = _filterDefinitionBuilder.Eq(existingItem => existingItem.Id, item.Id);
            _itemsCollection.ReplaceOne(filter, item);
        }

        public void DeleteItem(Guid id)
        {
            var filter = _filterDefinitionBuilder.Eq(item => item.Id, id);
            _itemsCollection.DeleteOne(filter);
        }
    }
}