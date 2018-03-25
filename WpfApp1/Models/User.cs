using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WpfApp1.Models
{
    class User
    {
        [BsonId]
//        [BsonRepresentation(BsonType.ObjectId)]
        public string Username;// { get; }
        public string Password;// { private get; set; }

        public User()
        {
        }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public bool CheckPassword(string pass)
        {
            return Password == pass;
        }
    }
}
