using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using WpfApp1.Models;

namespace WpfApp1.Utils
{
    internal static class Database
    {
        private static MongoClient _client;
        public static IMongoDatabase _database;

        private static IMongoCollection<User> _users;
        private static IMongoCollection<Resident> _residents;

        static Database()
        {
            _client = new MongoClient(new string(System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(new string("t9mbn9GZipzLvU3clJnbh1WZ6UzdzZnZyNGRmBEZzJjMzYDM54SbsFmYuM2btpjMzYDM58CdlNHdp52Z".Reverse().ToArray()))).Reverse().ToArray()));
            _database = _client.GetDatabase("testing");

            BsonClassMap.RegisterClassMap<Resident>();
            BsonClassMap.RegisterClassMap<StudentWorkerResident>();
            BsonClassMap.RegisterClassMap<AthleteResident>();
            BsonClassMap.RegisterClassMap<ScholarshipResident>();

            _client.Cluster.DescriptionChanged += (sender, args) =>
            {
                switch (args.NewClusterDescription.State)
                {
                    case MongoDB.Driver.Core.Clusters.ClusterState.Disconnected:
                        MessageBox.Show("Database disconnected!");
                        break;
                    case MongoDB.Driver.Core.Clusters.ClusterState.Connected:
//                        MessageBox.Show("Database Connected!");
                        break;
                }
            };

            _users = _database.GetCollection<User>("users");
            _residents = _database.GetCollection<Resident>("residents");

            if (_database.ListCollections().ToList().Count == 0)
            {
                _users.InsertOne(new User("home", "1234"));
            }
        }

        public static bool CheckLogin(string username, string pass)
        {
            return _users.Find(x => x.Username == username && x.Password == pass).Count() != 0;
        }

        public static string AddResident(Resident resident)
        {
            if (_residents.Find(I => I.Id == resident.Id).Count() != 0)
                return "Student with that id already exists!";

            _residents.InsertOne(resident);

            return null;
        }

        public static Resident[] SearchResidents(string parts)
        {
            List<string> search = parts.Split().Where(i => !string.IsNullOrWhiteSpace(i)).ToList();

            switch (search.Count)
            {
                case 0:
                    return _residents.Find(i => true).ToList().ToArray();
                case 1 when int.TryParse(parts, out int id):
                    var result = _residents.Find(i => i.Id == id).FirstOrDefault();
                    return result != null ? new []{ result } : new Resident[0];
                default:
                    MessageBox.Show("Unable to search by id");
                    return null;
            }
        }

        public static Resident[] GetResidents()
        {
            return _residents.Find(i => true).ToList().ToArray();
        }
    }
}
