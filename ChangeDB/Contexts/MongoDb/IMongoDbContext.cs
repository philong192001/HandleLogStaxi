using MongoDB.Driver;

namespace ChangeDB.Contexts.MongoDb;

public interface IMongoDbContext
{
    IMongoCollection<T> GetCollection<T>(string collectionName);

}
