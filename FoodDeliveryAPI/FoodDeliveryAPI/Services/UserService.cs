using AutoMapper;
using FoodDeliveryAPI.DTO;
using FoodDeliveryAPI.Infrastructure;
using FoodDeliveryAPI.Interfaces;
using FoodDeliveryAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace FoodDeliveryAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly FoodDeliveryDbContext _dbContext;
        private readonly IConfigurationSection _secretKey;
        public UserService(IConfiguration config,IMapper mapper, FoodDeliveryDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _secretKey = config.GetSection("SecretKey");
        }       
        public string Login(UserDTO dto, out UserDTO userData)
        {
            User user = _dbContext.Users.First(x => x.Username == dto.Username);
            userData = _mapper.Map<UserDTO>(user);

            if (user.Equals(null))
            {
                return null;
            }

            if (BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                List<Claim> claims = new List<Claim>();

                if (user.TypeOfUser.Equals("administrator"))
                    claims.Add(new Claim(ClaimTypes.Role, "administrator"));
                else if (user.TypeOfUser.Equals("potrosac"))
                    claims.Add(new Claim(ClaimTypes.Role, "potrosac"));
                else if (user.TypeOfUser.Equals("dostavljac"))
                    claims.Add(new Claim(ClaimTypes.Role, "dostavljac"));


                //Kreiramo kredencijale za potpisivanje tokena. Token mora biti potpisan privatnim kljucem
                //kako bi se sprecile njegove neovlascene izmene
                SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                        issuer: "http://localhost:44309", //url servera koji je izdao token
                        claims: claims, //claimovi
                        expires: DateTime.Now.AddMinutes(20), //vazenje tokena u minutama
                        signingCredentials: signingCredentials //kredencijali za potpis
                    );
                string tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return tokenString;
            }
            else
            {
                return null;
            }

        }

        public List<UserFullDTO> GetAllUsers()
        {
            return _mapper.Map<List<UserFullDTO>>(_dbContext.Users.ToList());
        }

        public List<UserFullDTO> GetAllDeliverers()
        {
            var result = _dbContext.Users.Where(x => x.TypeOfUser.Equals("dostavljac"));
            return _mapper.Map<List<UserFullDTO>>(result.ToList());
        }

        public UserFullDTO AddNewUser(UserFullDTO userDTO)
        {
            User user = _mapper.Map<User>(userDTO);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return _mapper.Map<UserFullDTO>(userDTO);
        }

        public void DeleteUser(long id)
        {
            User user = _dbContext.Users.Find(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
        }

        public UserFullDTO GetUserById(long id)
        {
            return _mapper.Map<UserFullDTO>(_dbContext.Users.Find(id));
        }

        public UserFullDTO GetUserByUsername(string username)
        {
            return _mapper.Map<UserFullDTO>(_dbContext.Users.First(x => x.Username == username));
        }

        public UserFullDTO UpdateUser(UserFullDTO userDTO, long id)
        {
            User user = _dbContext.Users.Find(id);
            if (user != null)
            {
                user.Username = userDTO.Username;
                user.Email = userDTO.Email;
                user.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
                user.Name = userDTO.Name;
                user.Surname = userDTO.Surname;
                user.DateOfBirth = userDTO.DateOfBirth;
                user.Address = userDTO.Address;
                user.TypeOfUser = userDTO.TypeOfUser;
                user.PhotoUrl = userDTO.PhotoUrl;
                user.Orders = userDTO.Orders;

                _dbContext.SaveChanges();
            }
            return _mapper.Map<UserFullDTO>(user);
        }

        public RegistrationDTO RegisterUser(RegistrationDTO registrationDTO)
        {
            User user = _mapper.Map<User>(registrationDTO);
            if (user != null)
            {
                user.Username = registrationDTO.Username;
                user.Email = registrationDTO.Email;
                user.Password = BCrypt.Net.BCrypt.HashPassword(registrationDTO.Password);
                user.Name = registrationDTO.Name;
                user.Surname = registrationDTO.Surname;
                user.DateOfBirth = registrationDTO.DateOfBirth;
                user.Address = registrationDTO.Address;
                user.TypeOfUser = registrationDTO.TypeOfUser;
                user.PhotoUrl = registrationDTO.PhotoUrl;

                if (user.TypeOfUser.Equals("dostavljac"))
                {
                    user.Verified = 0;
                    user.Registered = 3;
                }
                if (user.TypeOfUser.Equals("potrosac"))
                {
                    user.Verified = 3;
                    user.Registered = 0;
                }
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
            }
            return _mapper.Map<RegistrationDTO>(user);
        }

        public UserFullDTO ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            User user = _dbContext.Users.Find(changePasswordDTO.Id);
            if (user != null)
            {
                if(user.Password == BCrypt.Net.BCrypt.HashPassword(changePasswordDTO.OldPassword))
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordDTO.NewPassword);
                    _dbContext.SaveChanges();
                }
            }
            return _mapper.Map<UserFullDTO>(user);
        }

        public void SendMailServiceTest()
        {

           // string api_key = AIzaSyDYSN57e_aMVLON6Tu9xM7OT6FspktIOkE;

            //string[] Scopes = { GmailService.Scope.GmailReadonly };

            //string ApplicationName = "Gmail API .NET Quickstart";

            //try
            //{
            //    UserCredential credential;
            //    using (var stream = new FileStream("credentials.json", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            //    {
            //        string credPath = "token.json";
            //        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            //            GoogleClientSecrets.FromStream(stream).Secrets,
            //            Scopes,
            //            "user",
            //            CancellationToken.None,
            //            new FileDataStore(credPath, true)).Result;
            //        Console.WriteLine("Credential file saved to: " + credPath);

            //    }

            //    var service = new GmailService(new BaseClientService.Initializer
            //    {
            //        HttpClientInitializer = credential,
            //        ApplicationName = ApplicationName
            //    });

            //    UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");
            //    IList<Label> labels = request.Execute().Labels;
            //    Console.WriteLine("Labels:");
            //    if (labels == null || labels.Count == 0)
            //    {
            //        Console.WriteLine("No labels found.");
            //        return;
            //    }
            //    foreach (var labelItem in labels)
            //    {
            //        Console.WriteLine("{0}", labelItem.Name);
            //    }


            //}
            //catch (FileNotFoundException e)
            //{
            //    Console.WriteLine(e.Message);
            //}


            




            //string to = "marko4301@gmail.com"; //To address    
            //string from = "petarpetrovictest26@gmail.com"; //From address    
            //MailMessage mail = new MailMessage();
            //mail.To.Add(to);
            //mail.From = new MailAddress("petarpetrovictest26@gmail.com", "Email head", Encoding.UTF8);
            //mail.Subject = "Proba slanja mejla";
            //mail.SubjectEncoding = Encoding.UTF8;
            //mail.Body = "Bla BLa Bla";
            //mail.BodyEncoding = Encoding.UTF8;
            //mail.IsBodyHtml = true;
            //mail.Priority = MailPriority.High;
            //SmtpClient client = new SmtpClient();
            //client.Host = "smtp.gmail.com";
            //client.Port = 587;
            //client.EnableSsl = true;
            //client.UseDefaultCredentials = false;
            //client.Credentials = new NetworkCredential("petarpetrovictest26@gmail.com", "testMail123");
            //try
            //{
            //    client.Send(mail);
            //}
            //catch (Exception e)
            //{
            //    //
            //}
        }
    }
}
