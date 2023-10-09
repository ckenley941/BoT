using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Common.Data.Objects.Words;
using System.Text.Json;

namespace EnsenaMe.Data.MongoDB
{
    public class MongoDBService
    {

        private readonly IMongoCollection<Playlist> _playlistCollection;
        private readonly IMongoCollection<WordTranslationDto> _wordCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            //_playlistCollection = database.GetCollection<Playlist>(mongoDBSettings.Value.CollectionName);
            _wordCollection = database.GetCollection<WordTranslationDto>(mongoDBSettings.Value.CollectionName);
        }

        public async Task<List<Playlist>> GetAsync() 
        {
            return await _playlistCollection.Find(new BsonDocument()).ToListAsync();
        }
        public async Task CreateAsync(Playlist playlist) 
        {
            await _playlistCollection.InsertOneAsync(playlist);
            return;
        }

        public async Task AddWordAsync(WordTranslationDto word)
        {
            await _wordCollection.InsertOneAsync(word);
            var firstDocument = _wordCollection.Find(new BsonDocument()).FirstOrDefault();
            return;
        }

        public async Task AddToPlaylistAsync(string id, string movieId) 
        {
            FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
            UpdateDefinition<Playlist> update = Builders<Playlist>.Update.AddToSet<string>("movieIds", movieId);
            await _playlistCollection.UpdateOneAsync(filter, update);
            return;
        }
        public async Task DeleteAsync(string id) 
        {
            FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
            await _playlistCollection.DeleteOneAsync(filter);
            return;
        }

    }

    public class Playlist
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string username { get; set; } = null!;

        [BsonElement("items")]
        [JsonPropertyName("items")]
        public List<string> movieIds { get; set; } = null!;

    }
}
