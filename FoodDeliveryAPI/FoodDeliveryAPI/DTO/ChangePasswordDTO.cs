namespace FoodDeliveryAPI.DTO
{
    public class ChangePasswordDTO
    {
        public long Id { get; set; }
        public string OldPassword { get; set; } 
        public string NewPassword { get; set; } 
    }
}
