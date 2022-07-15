using FoodDeliveryAPI.Models;
using System.Text.Json.Serialization;

namespace FoodDeliveryAPI.DTO
{
    public class NewOrderDTO
    {
        public long Id { get; set; }
        public long UsersId { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public double TotalPrice { get; set; }
        public double DeliveryTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Delivered { get; set; } // 0 - not delivered; 1 - delivered
        public long DelivererId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

        public string ProductsIDs { get; set; }
    }
}
