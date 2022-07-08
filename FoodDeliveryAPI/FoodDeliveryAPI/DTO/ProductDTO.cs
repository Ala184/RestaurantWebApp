using FoodDeliveryAPI.Models;

namespace FoodDeliveryAPI.DTO
{
    public class ProductDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }

        public string PhotoUrl { get; set; }
        public string Ingredients { get; set; }

        public List<Order> Orders { get; set; }
    }
}
