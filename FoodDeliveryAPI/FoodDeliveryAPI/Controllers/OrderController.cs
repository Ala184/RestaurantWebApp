using FoodDeliveryAPI.DTO;
using FoodDeliveryAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize(Roles = "potrosac")]
        public IActionResult AddNewOrder([FromBody] NewOrderDTO order)
        {
            try
            {
                return Ok(_orderService.AddNewOrder(order));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "administrator, dostavljac")] //ovako treba, ako imas vise razlicitih rola 
        public IActionResult GetAllOrders()
        {
            try
            {
                return Ok(_orderService.GetAllOrders());
            }
            catch (Exception e)
            {
                return BadRequest((string)e.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "administrator, dostavljac, potrosac")]
        public IActionResult GetOrderById(long id)
        {
            try
            {
                return Ok(_orderService.GetOrderById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("ordersOfUser/{usersId}")]
        [Authorize(Roles = "administrator, dostavljac, potrosac")]
        public IActionResult GetAllOrdersOfUser(long usersId)
        {
            try
            {
                return Ok(_orderService.GetAllOrdersOfUser(usersId));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("deliverersDoneOrders/{deliverersId}")]
        [Authorize(Roles = "dostavljac")]
        public IActionResult GetDeliverersDoneOrders(long deliverersId)
        {
            try
            {
                return Ok(_orderService.GetDeliverersDoneOrders(deliverersId));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("deliverersCurrentOrder/{deliverersId}")]
        [Authorize(Roles = "dostavljac")]
        public IActionResult GetDeliverersCurentOrder(long deliverersId)
        {
            try
            {
                return Ok(_orderService.GetDeliverersCurentOrder(deliverersId));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("consumersCurrentOrder/{consumersId}")]
        [Authorize(Roles = "potrosac")]
        public IActionResult GetConsumersCurentOrder(long consumersId)
        {
            try
            {
                return Ok(_orderService.GetConsumersCurentOrder(consumersId));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("orderBids")]
        [Authorize(Roles = "dostavljac")]
        public IActionResult GetOrderBids()
        {
            try
            {
                return Ok(_orderService.GetOrderBids());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpPut("takeOrder/{orderId}/{delivererId}")]
        [Authorize(Roles = "dostavljac")]
        public IActionResult TakeOrder(long orderId, long delivererId)
        {
            try
            {
                OrderDTO result = _orderService.TakeOrder(orderId, delivererId);
                if (result == null)
                {
                    return BadRequest("Imate nedostavljenu narudzbu.");
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("orderIsDelivered/{orderId}")]
        [Authorize(Roles ="dostavljac")]
        public IActionResult OrderIsDelivered(long orderId)
        {
            try
            {
                return Ok(_orderService.OrderIsDelivered(orderId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
