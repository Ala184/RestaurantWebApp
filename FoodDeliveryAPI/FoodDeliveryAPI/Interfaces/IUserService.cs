using FoodDeliveryAPI.DTO;

namespace FoodDeliveryAPI.Interfaces
{
    public interface IUserService
    {
        string Login(UserDTO dto, out UserDTO userData);
        List<UserFullDTO> GetAllUsers();

        List<UserFullDTO> GetAllDeliverers();
        UserFullDTO AddNewUser(UserFullDTO userDTO);

        RegistrationDTO RegisterUser(RegistrationDTO registrationDTO);
        void DeleteUser(long id);

        UserFullDTO GetUserById(long id);
        UserFullDTO GetUserByUsername(string username);

        UserFullDTO UpdateUser(UserFullDTO userDTO, long id);

        UserFullDTO ChangePassword(ChangePasswordDTO changePasswordDTO);

       // void SendMailServiceTest();

        bool CheckIfApproved(long id);

    }
}
