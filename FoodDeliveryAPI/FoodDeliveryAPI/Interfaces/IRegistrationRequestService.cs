using FoodDeliveryAPI.DTO;

namespace FoodDeliveryAPI.Interfaces
{
    public interface IRegistrationRequestService
    {
        List<RegistrationRequestDTO> GetAllRegistrationRequests();

        //RegistrationRequestDTO AddNewRegistrationRequest(RegistrationRequestDTO registrationRequestDTO);

        //void DeleteRegistrationRequest(long id);

        //RegistrationRequestDTO GetRegistrationRequestById(long id);

        //RegistrationRequestDTO UpdateRegistrationRequest(RegistrationRequestDTO registrationRequestDTO, long id);

        RegistrationRequestDTO AcceptRegistrationRequest(long id);
        RegistrationRequestDTO DeclineRegistrationRequest(long id);
    }
}
