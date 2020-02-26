using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarCrud.Models
{
    public class Car
    {

        public Car(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }
    }
}
