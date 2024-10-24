using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AppAspnetMvc.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? id { get; set; }

        [Display(Name = "Nome")]
        public string? name { get; set; }

        [Display(Name = "Descrição")]
        public string? description { get; set; }

        [Display(Name = "Preço")]
        public double? price { get; set; }
    }
}
