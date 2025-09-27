namespace FlowerShop.Models
{
    public class FlowerCategory
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<Flower> Flowers { get; set; } = new();

        public FlowerCategory() { }
    }
}
