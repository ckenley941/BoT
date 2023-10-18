using MongoDB.Bson;
using MongoDB.Driver;

namespace EnsenaMe.Data.MongoDB
{
    public static class MongoDBConnection
    {
        public static void ConnectToDb()
        {
            MongoClient dbClient = new MongoClient("mongodb+srv://admin:Castaway14@cluster-tor.umtmzfd.mongodb.net/\r\n\r\n");

            var database = dbClient.GetDatabase("sample_training");
            var collection = database.GetCollection<BsonDocument>("grades");
            var firstDocument = collection.Find(new BsonDocument()).FirstOrDefault();
            Console.WriteLine(firstDocument.ToString());

        }
    }
}