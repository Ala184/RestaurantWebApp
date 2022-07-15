using AutoMapper;
using FoodDeliveryAPI.DTO;
using FoodDeliveryAPI.Infrastructure;
using FoodDeliveryAPI.Interfaces;
using FoodDeliveryAPI.Models;

namespace FoodDeliveryAPI.Services
{
    public class RegistrationService : IRegistrationRequestService
    {
        private readonly IMapper _mapper;
        private readonly FoodDeliveryDbContext _dbContext;
        private readonly IEmailSender _emailSender;

        public RegistrationService(IMapper mapper, FoodDeliveryDbContext dbContext, IEmailSender emailSender)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _emailSender = emailSender; 
        }

        public List<RegistrationRequestDTO> GetAllRegistrationRequests()
        {
           // var users = _dbContext.Users.Where(x => x.Registered == 0 );
            var users = _dbContext.Users;

            return _mapper.Map<List<RegistrationRequestDTO>>(users);
        }

        public RegistrationRequestDTO AcceptRegistrationRequest(long id)
        {
            User user = _dbContext.Users.Find(id);
            if (user.Registered != 1)
            {
                user.Registered = 1;
                string subject = "FoodDeliveryAPP - Registration info";
                string message = $"Poštovani {user.Username}, vaš zahtev za registraciju je odobren. Hvala na poverenju.";
                _emailSender.SendMail(subject, message, user.Email);
                _dbContext.SaveChanges();
            }
            return _mapper.Map<RegistrationRequestDTO>(user);
        }

        public RegistrationRequestDTO DeclineRegistrationRequest(long id)
        {
            User user = _dbContext.Users.Find(id);
            if (user.Registered != 2)
            {
                user.Registered = 2; 
                string subject = "FoodDeliveryAPP - Registration info";
                string message = $"Poštovani {user.Username}, vaš zahtev za registraciju je odbijen. Za više informacija kontaktirajte +38166111222.";
                _emailSender.SendMail(subject, message, user.Email);
                _dbContext.SaveChanges();
            }
            return _mapper.Map<RegistrationRequestDTO>(user);
        }

       
        //public List<RegistrationRequestDTO> GetAllRegistrationRequests()
        //{
        //    return _mapper.Map<List<RegistrationRequestDTO>>(_dbContext.RegistrationRequests.ToList());
        //}

        //public RegistrationRequestDTO GetRegistrationRequestById(long id)
        //{
        //    return _mapper.Map<RegistrationRequestDTO>(_dbContext.RegistrationRequests.Find(id));
        //}

        //public RegistrationRequestDTO AddNewRegistrationRequest(RegistrationRequestDTO registrationDTO)
        //{
        //    RegistrationRequest regReq = _mapper.Map<RegistrationRequest>(registrationDTO);
        //    _dbContext.RegistrationRequests.Add(regReq);
        //    _dbContext.SaveChanges();

        //    return _mapper.Map<RegistrationRequestDTO>(registrationDTO);
        //}

        //public void DeleteRegistrationRequest(long id)
        //{
        //    RegistrationRequest regReq = _dbContext.RegistrationRequests.Find(id);
        //    if (regReq != null)
        //    {
        //        _dbContext.RegistrationRequests.Remove(regReq);
        //        _dbContext.SaveChanges();
        //    }
        //}

        //public RegistrationRequestDTO UpdateRegistrationRequest(RegistrationRequestDTO registrationDTO, long id)
        //{
        //    RegistrationRequest regReq = _dbContext.RegistrationRequests.Find(id);
        //    if (regReq != null)
        //    {
        //        regReq.UserName = registrationDTO.UserName;
        //        regReq.Status = registrationDTO.Status;
        //        _dbContext.SaveChanges();
        //    }
        //    return _mapper.Map<RegistrationRequestDTO>(regReq);
        //}

        //public RegistrationRequestDTO AcceptRegistrationRequest(long id)
        //{
        //    RegistrationRequest regReq = _dbContext.RegistrationRequests.Find(id);
        //    if (regReq != null)
        //    {                
        //        regReq.Status = 1;
        //        _dbContext.SaveChanges();
        //    }
        //    return _mapper.Map<RegistrationRequestDTO>(regReq);
        //}

        //public RegistrationRequestDTO DeclineRegistrationRequest(long id)
        //{
        //    RegistrationRequest regReq = _dbContext.RegistrationRequests.Find(id);
        //    if (regReq != null)
        //    {
        //        regReq.Status = 2;
        //        _dbContext.SaveChanges();
        //    }
        //    return _mapper.Map<RegistrationRequestDTO>(regReq);
        //}

    }
}
