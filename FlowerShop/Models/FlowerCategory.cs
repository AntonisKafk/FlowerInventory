namespace FlowerShop.Models
{
    public class FlowerCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FlowerCategory(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
