using System.Text.Json.Serialization;

namespace FoodDeliveryAPI.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }

        public string PhotoUrl { get; set; }
        public string Ingredients { get; set; }

        public List<Order> Orders { get; set; }

    }
}
