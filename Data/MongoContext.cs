using System;
using Data.Helpers.Mongo;
using Models;
using MongoDB.Driver;

namespace Data
{
    public class MongoContext
    {
        private const string USER_COLLECTION_NAME = "users";
        private const string TODO_COLLECTION_NAME = "todo-items";

        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private bool _collectionsPrepared;

        public IMongoCollection<User> UsersCollection { get; set; }
        public IMongoCollection<Todo> TodoCollection { get; set; }
        
        public MongoContext(IDatabaseSettings settings)
        {
            _client = new MongoClient(settings.ConnectionString);
            _database = _client.GetDatabase(settings.DatabaseName);
            _collectionsPrepared = false;
        }

        public IMongoCollection<T> Set<T>() {
            var typeToSearch = typeof(T);

            if (!_collectionsPrepared) {
                PrepareCollections();
            }
            
            if (typeToSearch == typeof(User)) {
                return (IMongoCollection<T>) UsersCollection;
            }
            else if (typeToSearch == typeof(Todo)) {
                return (IMongoCollection<T>) TodoCollection;
            }
            else {
                throw new InvalidOperationException("This type to set isn't supported");
            }
        }

        private void PrepareCollections() {            
            UsersCollection = _database.GetCollection<User>(USER_COLLECTION_NAME);
            TodoCollection = _database.GetCollection<Todo>(TODO_COLLECTION_NAME);
            
            _collectionsPrepared = true;
        }
    }
}