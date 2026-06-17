using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities
{
    [BsonIgnoreExtraElements]
    public class ProductBrand
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
       
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
