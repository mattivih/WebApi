using MongoDB.Driver;

namespace WebApiAllAssignments.Mongodb
{
    public class MongoDbClient
    {
        private readonly MongoClient _mongoClient;

        public MongoDbClient()
        {
            //Mongo client holds the connection to the database
            _mongoClient = new MongoClient("mongodb://localhost:27017");
        }

        public IMongoDatabase GetDatabase(string name)
        {
            return _mongoClient.GetDatabase(name);
        }
    }
}