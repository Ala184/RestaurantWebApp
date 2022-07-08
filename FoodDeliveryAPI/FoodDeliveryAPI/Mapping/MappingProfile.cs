using AutoMapper;
using FoodDeliveryAPI.DTO;
using FoodDeliveryAPI.Models;

namespace FoodDeliveryAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap(); //Kazemo mu da mapira Subject na SubjectDto i obrnuto

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserFullDTO>().ReverseMap();
            CreateMap<User, RegistrationDTO>().ReverseMap();
            CreateMap<User, VerificationDTO>().ReverseMap();
            CreateMap<User, RegistrationRequestDTO>().ReverseMap();


            CreateMap<Order, OrderDTO>().ReverseMap();
        }
    }
}
