using AutoMapper;
using FoodDeliveryAPI.Infrastructure;
using FoodDeliveryAPI.DTO;
using FoodDeliveryAPI.Models;
using FoodDeliveryAPI.Interfaces;

namespace FoodDeliveryAPI.Services
{
    public class VerificationRequestService : IVerificationRequestService
    {
        private readonly IMapper _mapper;
        private readonly FoodDeliveryDbContext _dbContext;
        private readonly IEmailSender _emailSender;

        public VerificationRequestService(IMapper mapper, FoodDeliveryDbContext dbContext, IEmailSender emailSender)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _emailSender = emailSender; 
        }

        public List<VerificationDTO> GetAllVerificationRequests()
        {
            var users = _dbContext.Users.Where(x => x.Verified == 0);            

            return _mapper.Map<List<VerificationDTO>>(users);
        }

        public VerificationDTO RefuseVerificationRequest(long id)
        {
            User user = _dbContext.Users.Find(id);
            if (user.Verified != 2)
            {
                user.Verified = 2; 
                string subject = "FoodDeliveryAPP - Registration info";
                string message = $"Poštovani {user.Username}, vaš zahtev za verifikaciju je odbijen. Za više informacija kontaktirajte +38166111222. ";
               // _emailSender.SendMail(subject, message, user.Email);
                _dbContext.SaveChanges();
            }
            return _mapper.Map<VerificationDTO>(user);
        }

        public VerificationDTO VerifyVerificationRequest(long id)
        {
            User user = _dbContext.Users.Find(id);
            if (user.Verified != 1)
            {
                user.Verified = 1;
                string subject = "FoodDeliveryAPP - Registration info";
                string message = $"Poštovani {user.Username}, vaš zahtev za verifikaciju je odobren. Hvala na poverenju.";
             //   _emailSender.SendMail(subject, message, user.Email);
                _dbContext.SaveChanges();
            }
            return _mapper.Map<VerificationDTO>(user);
        }
        //public List<VerificationDTO> GetAllVerificationRequests()
        //{
        //    return _mapper.Map<List<VerificationDTO>>(_dbContext.VerificationRequests.ToList());
        //}

        //public VerificationDTO GetVerificationRequestById(long id)
        //{
        //    return _mapper.Map<VerificationDTO>(_dbContext.VerificationRequests.Find(id));
        //}

        //public VerificationDTO AddNewVerificationRequest(VerificationDTO verificationDTO)
        //{
        //    VerificationRequest verReq = _mapper.Map<VerificationRequest>(verificationDTO);
        //    _dbContext.VerificationRequests.Add(verReq);
        //    _dbContext.SaveChanges();

        //    return _mapper.Map<VerificationDTO>(verificationDTO);
        //}

        //public void DeleteVerificationRequest(long id)
        //{
        //    VerificationRequest verReq = _dbContext.VerificationRequests.Find(id);
        //    if (verReq != null)
        //    {
        //        _dbContext.VerificationRequests.Remove(verReq);
        //        _dbContext.SaveChanges();
        //    }
        //}

        //public VerificationDTO UpdateVerificationRequest(VerificationDTO verificationDTO, long id)
        //{
        //    VerificationRequest verReq = _dbContext.VerificationRequests.Find(id);
        //    if (verReq != null)
        //    {
        //        verReq.DelivererUserName = verificationDTO.DelivererUserName;
        //        verReq.Status = verificationDTO.Status;
        //        _dbContext.SaveChanges();
        //    }
        //    return _mapper.Map<VerificationDTO>(verReq);
        //}

        //public VerificationDTO VerifyVerificationRequest(long id)
        //{
        //    VerificationRequest verReq = _dbContext.VerificationRequests.Find(id);
        //    if (verReq != null)
        //    {
        //        verReq.Status = 1;
        //        _dbContext.SaveChanges();
        //    }
        //    return _mapper.Map<VerificationDTO>(verReq);
        //}

    }
}
