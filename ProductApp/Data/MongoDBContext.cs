using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.Data
{
    public class MongoDBContext : Controller
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly String SubID;

        public MongoDBContext(IOptions<MongoDBSettings> settings)
        {
            _client = new MongoClient(settings.Value.ConnectionString);
            _database = _client.GetDatabase(settings.Value.DatabaseName);
        }


        public MongoDBContext(IOptions<MongoDBSettings> settings, String _SubID)
        {
            _client = new MongoClient(settings.Value.ConnectionString);
            _database = _client.GetDatabase(settings.Value.DatabaseName);
            SubID = _SubID;
        }

       

        public IMongoCollection<Products> productsTable
        {
            get
            {
                return _database.GetCollection<Products>("product");
            }
        }


    }
}
 