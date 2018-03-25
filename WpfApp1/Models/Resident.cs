using MongoDB.Bson.Serialization.Attributes;

namespace WpfApp1.Models
{
    public enum ResidentType
    {
        Scholarship,
        Athlete,
        StudentWorker
    }

    abstract class Resident
    {
        [BsonId]
        public string Name => FirstName + ' ' + LastName;

        public uint Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public uint DormRoom { get; set; }
        public uint DormFloor { get; set; }

        public abstract float Rent { get; }
    }
}
