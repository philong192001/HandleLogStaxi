using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace ChangeDB.Contexts.MongoDb;

public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly IMongoClient _client;
    private readonly IConfiguration _configuration;

    public MongoDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _client = new MongoClient(_configuration.GetSection("MongoDbSettings:ConnectionString").Value);
        _database = _client.GetDatabase(_configuration.GetSection("MongoDbSettings:DatabaseName").Value);
        RegisterConventions();

    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }

    private void RegisterConventions()
    {
        //Ignore những trường không cần thiết - không cần map 1-1 class với obj dưới db
        var conventionPackClass = new ConventionPack { new IgnoreExtraElementsConvention(true) };
        ConventionRegistry.Register("IgnoreExtraElements", conventionPackClass, type => true);
        //Không insert những field null
        var conventionPack = new ConventionPack
        {
            new IgnoreIfNullConvention(true)
        };
        ConventionRegistry.Register("IgnoreNulls", conventionPack, t => true);
    }
}
