using System.Text.Json.Serialization;

namespace FlowerShop.Models
{
    public class Flower
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        [JsonIgnore]
        public FlowerCategory? Category { get; set; }
    }
}
