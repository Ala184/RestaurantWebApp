using AutoMapper;
using FoodDeliveryAPI.DTO;
using FoodDeliveryAPI.Infrastructure;
using FoodDeliveryAPI.Interfaces;
using FoodDeliveryAPI.Models;

namespace FoodDeliveryAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly FoodDeliveryDbContext _dbContext;

        public OrderService(IMapper mapper, FoodDeliveryDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public List<OrderDTO> GetAllOrders()
        {
            var result = _mapper.Map<List<OrderDTO>>(_dbContext.Orders.ToList());
            // return _mapper.Map<List<OrderDTO>>(_dbContext.Orders.ToList());
            return result;
        }

        public OrderDTO GetOrderById(long id)
        {
            return _mapper.Map<OrderDTO>(_dbContext.Orders.Find(id));
        }

        public OrderDTO AddNewOrder(OrderDTO orderDTO)
        {
            Order order = _mapper.Map<Order>(orderDTO);
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return _mapper.Map<OrderDTO>(orderDTO);
        }

        public void DeleteOrder(long id)
        {
            Order order = _dbContext.Orders.Find(id);
            if (order != null)
            {
                _dbContext.Orders.Remove(order);
                _dbContext.SaveChanges();
            }
        }

        public OrderDTO UpdateOrder(OrderDTO orderDTO, long id)
        {
            Order order = _dbContext.Orders.Find(id);
            if (order != null)
            {
                order.Address = orderDTO.Address;
                order.Comment = orderDTO.Comment;
                order.TotalPrice = orderDTO.TotalPrice;
                order.DeliveryTime = orderDTO.DeliveryTime;
                order.UsersId = orderDTO.UsersId;
                order.Products = orderDTO.Products;
                _dbContext.SaveChanges();
            }
            return _mapper.Map<OrderDTO>(order);
        }

        public List<OrderDTO> GetAllOrdersOfUser(long userId)
        {
            return _mapper.Map<List<OrderDTO>>(_dbContext.Orders.Where(x=> x.UsersId == userId));
        }

        public List<OrderDTO> GetOrderBids()
        {
            return _mapper.Map<List<OrderDTO>>(_dbContext.Orders.Where(x => x.DelivererId.Equals(0) && x.Delivered.Equals(0)));
        }

        public OrderDTO TakeOrder(long orderId, long delivererId)
        {
            Order undeliveredOrder = _dbContext.Orders.FirstOrDefault(x => x.DelivererId.Equals(delivererId) && x.Delivered.Equals(0));
            if (undeliveredOrder != null)
                return null;


            Order order = _dbContext.Orders.Find(orderId);
            if (order != null)
            {
                order.DelivererId = delivererId;
            }
            _dbContext.SaveChanges();
            return _mapper.Map<OrderDTO>(order);
        }

        public OrderDTO OrderIsDelivered(long orderId)
        {
            Order order = _dbContext.Orders.Find(orderId);
            if (order != null)
            {
                order.Delivered = 1;
            }
            _dbContext.SaveChanges();
            return _mapper.Map<OrderDTO>(order);
        }

        public List<OrderDTO> GetDeliverersDoneOrders(long delivererId)
        {
            List<Order> deliverersOrders = _dbContext.Orders
                                            .Where(x => x.DelivererId.Equals(delivererId) && x.Delivered.Equals(1))
                                            .ToList();
            return _mapper.Map<List<OrderDTO>>(deliverersOrders);
        }

        public OrderDTO GetDeliverersCurentOrder(long deliverersId)
        {
            Order currentOrder = _dbContext.Orders.FirstOrDefault(x => x.DelivererId.Equals(deliverersId) && x.Delivered.Equals(0));

            return _mapper.Map<OrderDTO>(currentOrder);
        }

        public OrderDTO GetConsumersCurentOrder(long consumersId)
        {
            Order currentOrder = _dbContext.Orders.FirstOrDefault(x => x.UsersId.Equals(consumersId) && x.Delivered.Equals(0));

            return _mapper.Map<OrderDTO>(currentOrder);
        }
    }
}
