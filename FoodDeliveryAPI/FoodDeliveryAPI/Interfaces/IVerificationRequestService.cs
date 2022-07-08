using FoodDeliveryAPI.DTO;

namespace FoodDeliveryAPI.Interfaces
{
    public interface IVerificationRequestService
    {
        List<VerificationDTO> GetAllVerificationRequests();

        //VerificationDTO AddNewVerificationRequest(VerificationDTO verificationDTO);

        //void DeleteVerificationRequest(long id);

        //VerificationDTO GetVerificationRequestById(long id);

        //VerificationDTO UpdateVerificationRequest(VerificationDTO verificationDTO, long id);

        VerificationDTO VerifyVerificationRequest(long id);

        VerificationDTO RefuseVerificationRequest(long id);
    }
}
