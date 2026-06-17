using Catalog.Core.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Application.Responses
{
    [BsonIgnoreExtraElements]
    public class ProductResponseDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        public string Summary { get; set; }
        public string ImageFile { get; set; }
        public ProductType Type { get; set; }
        public ProductBrand Brand { get; set; }
    }
}
