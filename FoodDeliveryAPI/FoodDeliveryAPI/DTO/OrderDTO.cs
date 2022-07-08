using FoodDeliveryAPI.Models;

namespace FoodDeliveryAPI.DTO
{
    public class OrderDTO
    {
        public long Id { get; set; }
        public long UsersId { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public double TotalPrice { get; set; }
        public double DeliveryTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Delivered { get; set; }
        public long DelivererId { get; set; }
        public List<Product> Products { get; set; }
        public User User { get; set; }
    }
}
