using FoodDeliveryAPI.DTO;
using FoodDeliveryAPI.Interfaces;
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

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            return Ok(_orderService.GetAllOrders());
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(long id)
        {
            return Ok(_orderService.GetOrderById(id));
        }

        [HttpGet("ordersOfUser/{usersId}")]
        public IActionResult GetAllOrdersOfUser(long usersId)
        {
            try
            {
                return Ok(_orderService.GetAllOrdersOfUser(usersId));

            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("deliverersDoneOrders/{deliverersId}")]
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

        [HttpPost]
        public IActionResult CreateProduct([FromBody] OrderDTO order)
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

        [HttpPut("takeOrder/{orderId}/{delivererId}")]
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
