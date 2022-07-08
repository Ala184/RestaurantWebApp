using System.Text.Json.Serialization;

namespace FoodDeliveryAPI.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string TypeOfUser { get; set; }
        public string PhotoUrl { get; set; }

        public int Registered { get; set; } // 0 - waiting registration; 1 - registered; 2 - declined; 3 - user is deliverer
        public int Verified { get; set; }  //1 - waiting verification; 1 - verified; 2 - declined; 3 - user is consumer

        [JsonIgnore]
        public List<Order> Orders { get; set; }

        //public string GetHash(string pw)
        //{
        //    return BCrypt.Net.BCrypt.HashPassword(pw);
        //}

    }
}
