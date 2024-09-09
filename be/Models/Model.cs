
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
public class Model
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string RandomText { get; set; }
}