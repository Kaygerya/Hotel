using ERPManager.Core.Entities;
using ERPManager.DataAccess.Abstract;
using ERPManager.DataAccess.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPManager.Core.Model
{
    public class MongoRepository : IRepository
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public MongoRepository(IOptions<ConfigurationOptions> optionsAccessor)
        {
            var configurationOptions = optionsAccessor.Value;
            _client = new MongoClient("mongodb://95.154.201.126");
            _database = _client.GetDatabase("main");
        }

        public IQueryable<T> All<T>() where T : Entity, new()
        {
            return _database.GetCollection<T>(typeof(T).Name).AsQueryable();
        }

        public IQueryable<T> Where<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : Entity, new()
        {
            return All<T>().Where(expression);
        }

        public void Delete<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate) where T : Entity, new()
        {
            var result = _database.GetCollection<T>(typeof(T).Name).DeleteMany(predicate);

        }
        public T Single<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : Entity, new()
        {
            return All<T>().Where(expression).SingleOrDefault();
        }

        public bool CollectionExists<T>() where T : Entity, new()
        {
            var collection = _database.GetCollection<T>(typeof(T).Name);
            var filter = new BsonDocument();
            var totalCount = collection.CountDocuments(filter);
            return (totalCount > 0) ? true : false;

        }

        public void Add<T>(T item) where T : Entity, new()
        {
            _database.GetCollection<T>(typeof(T).Name).InsertOne(item);
        }

        public void Add<T>(IEnumerable<T> items) where T : Entity, new()
        {
            _database.GetCollection<T>(typeof(T).Name).InsertMany(items);
        }

        public void Update<T>(T item) where T : Entity, new()
        {
            _database.GetCollection<T>(typeof(T).Name).ReplaceOne(k=> k.Id == item.Id, item);
        }

    }
}
