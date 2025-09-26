namespace FlowerShop.Models
{
    public class Flower
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }

        public Flower(int id, string name, int categoryId, decimal price)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
            Price = price;
        }

    }
}
