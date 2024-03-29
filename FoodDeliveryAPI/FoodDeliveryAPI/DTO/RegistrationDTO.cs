﻿namespace FoodDeliveryAPI.DTO
{
    public class RegistrationDTO
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
    }
}
