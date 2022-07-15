using FoodDeliveryAPI.DTO;
using FoodDeliveryAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        private readonly IVerificationRequestService _verificationService;

        public VerificationController(IVerificationRequestService verificationService)
        {
            _verificationService = verificationService;
        }

        [HttpGet]
        [Authorize(Roles = "administrator")]
        public IActionResult GetAllVerificationRequests()
        {
            try
            {
                return Ok(_verificationService.GetAllVerificationRequests());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[HttpPost]
        //public IActionResult AddNewVerificationRequest(VerificationDTO dto)
        //{
        //    return Ok(_verReqService.AddNewVerificationRequest(dto));
        //}

        [HttpPut("verify/{id}")]
        [Authorize(Roles = "administrator")]
        public IActionResult AcceptRegistrationRequest(long id)
        {
            try
            {
                return Ok(_verificationService.VerifyVerificationRequest(id)); 

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("refuseVerification/{id}")]
        [Authorize(Roles = "administrator")]
        public IActionResult RefuseRegistrationRequest(long id)
        {
            try
            {
                return Ok(_verificationService.RefuseVerificationRequest(id));

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
