using FoodDeliveryAPI.DTO;
using FoodDeliveryAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Post([FromBody] UserDTO dto)
        {
            try
            {
                UserDTO userDto;
                string token = _userService.Login(dto, out userDto);
                if(token == null)
                    return BadRequest("Pogresna lozinka.");

                return Ok(new { Token = token, UserData = userDto });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);   
            }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegistrationDTO dto)
        {
            return Ok(_userService.RegisterUser(dto));    
        }

        [HttpGet]
        public async Task<ActionResult<List<UserFullDTO>>> GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet("deliverers")]
        public async Task<ActionResult<List<UserFullDTO>>> GetAllDeliverers()
        {
            return Ok(_userService.GetAllDeliverers());
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(long id)
        {
            return Ok(_userService.GetUserById(id));
        }

        [HttpGet("profile/{username}")]
        public IActionResult GetUserByUsername(string username)
        {
            try
            {
                return Ok(_userService.GetUserByUsername(username));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateUser([FromBody] UserFullDTO newUserData, long id)
        {
            int o = 0;
            try
            {
                return Ok(_userService.UpdateUser(newUserData, id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }

        [HttpPut("changePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDTO dto)
        {
            try
            {
                return Ok(_userService.ChangePassword(dto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("nesto-administratorsko")]
        [Authorize(Roles = "administrator")]
        public IActionResult NestoAdministratorsko()
        {
            return Ok("Administrator si i mozes da vidis ovo, bravo za tebe!");
        }

        [HttpGet("nesto-potrosacko")]
        [Authorize(Roles = "potrosac")]
        public IActionResult NestoPotrosacko()
        {
            return Ok("Potrosac si i mozes da vidis ovo, bravo za tebe!");
        }

        [HttpGet("nesto-dostavljacko")]
        [Authorize(Roles = "dostavljac")]
        public IActionResult NestoDostavljacko()
        {
            return Ok("Dostavljac si i mozes da vidis ovo, bravo za tebe!");
        }

        [HttpGet("posalji-mejl")]
        public IActionResult SaljiMejl()
        {
            try
            {
                _userService.SendMailServiceTest();
                return Ok("Mejl je poslat");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
