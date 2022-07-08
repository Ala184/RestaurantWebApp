using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodDeliveryAPI.Interfaces;
using FoodDeliveryAPI.DTO;

namespace FoodDeliveryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationRequestController : ControllerBase
    {
        private readonly IRegistrationRequestService _registrationRequestService;

        public RegistrationRequestController(IRegistrationRequestService registrationRequestService)
        {
            _registrationRequestService = registrationRequestService;
        }

        [HttpGet]
        public IActionResult GetAllRegistrationRequests()
        {
            try
            {
                return Ok(_registrationRequestService.GetAllRegistrationRequests());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("accept/{id}")]
        public IActionResult AcceptRegistrationRequest(long id)
        {
            return Ok(_registrationRequestService.AcceptRegistrationRequest(id));
        }

        [HttpPut("decline/{id}")]
        public IActionResult DeclineRegistrationRequest(long id)
        {
            return Ok(_registrationRequestService.DeclineRegistrationRequest(id));
        }

        //[HttpGet]
        //public async Task<ActionResult<List<RegistrationRequestDTO>>> GetAllRegistrationRequests()
        //{
        //    return Ok(_registrationRequestService.GetAllRegistrationRequests());
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetRegistrationRequestById(long id)
        //{
        //    return Ok(_registrationRequestService.GetRegistrationRequestById(id));
        //}



    }
}
