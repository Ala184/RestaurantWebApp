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
                string token = _userService.Login(dto);
                if(token == null)
                    return BadRequest("Pogresna lozinka.");

                return Ok(new { Token = token});
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
        [Authorize(Roles = "administrator")]
        public async Task<ActionResult<List<UserFullDTO>>> GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet("deliverers")]
        [Authorize(Roles = "administrator")]
        public async Task<ActionResult<List<UserFullDTO>>> GetAllDeliverers()
        {
            return Ok(_userService.GetAllDeliverers());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "administrator, dostavljac, potrosac")]
        public IActionResult GetUserById(long id)
        {
            return Ok(_userService.GetUserById(id));
        }

        [HttpGet("profile/{username}")]
        [Authorize(Roles = "administrator, dostavljac, potrosac")]
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
        [Authorize(Roles = "administrator, dostavljac, potrosac")]
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
        [Authorize(Roles = "administrator, potrosac, dostavljac")]
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

        [HttpGet("checkIfApproved/{id}")]
        public IActionResult CheckIfApproved(long id)
        {
            try
            {
                return Ok(_userService.CheckIfApproved(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
