using AutoMapper;
using FoodDeliveryAPI.DTO;
using FoodDeliveryAPI.Infrastructure;
using FoodDeliveryAPI.Interfaces;
using FoodDeliveryAPI.Models;
using Microsoft.EntityFrameworkCore;

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
            List<Order> orders = _dbContext.Orders.ToList();
            List<OrderDTO> orderDTOs = new List<OrderDTO>();
            foreach (Order o in orders)
            {
                OrderDTO orderDTO = new OrderDTO();
                orderDTO = myMapper(o, orderDTO); //napravimo kpiju ordera koja sadrzi proizvode

                List<int> productIDs = new List<int>(); //nadjemo koji su to sve proizvodi
                foreach (var x in o.ProductsIDs.Split(','))
                {
                    int id = Int32.Parse(x);
                    productIDs.Add(id);
                }

                foreach (int id in productIDs)
                {
                    ProductDTO p = _mapper.Map<ProductDTO>(_dbContext.Products.Find((long)id));
                    orderDTO.Products.Add(p);
                }
                orderDTOs.Add(orderDTO);
            }
            return orderDTOs;

            //var result = _mapper.Map<List<OrderDTO>>(_dbContext.Orders.ToList());
            // return _mapper.Map<List<OrderDTO>>(_dbContext.Orders.ToList());
            //return result;
        }

        public OrderDTO GetOrderById(long id)
        {
            Order order = _dbContext.Orders.Find(id);
            if (order == null)
                return null;
            OrderDTO retVal = new OrderDTO();
            retVal = myMapper(order, retVal);

            List<int> productIDs = new List<int>(); //nadjemo koji su to sve proizvodi
            foreach (var x in order.ProductsIDs.Split(','))
            {
                int id2 = Int32.Parse(x);
                productIDs.Add(id2);
            }

            foreach (int id2 in productIDs)
            {
                ProductDTO p = _mapper.Map<ProductDTO>(_dbContext.Products.Find((long)id2));
                retVal.Products.Add(p);
            }

            User user = _dbContext.Users.Find(order.UsersId);
            if (user != null)
                order.User = user;

            return retVal;
        }

        public OrderDTO AddNewOrder(NewOrderDTO orderDTO)
        {
            Order order = _mapper.Map<Order>(orderDTO);
            if (order == null)
                return null;
            User user = _mapper.Map<User>(_dbContext.Users.Find(orderDTO.UsersId));

            order.UsersId = user.Id;
            order.User = user;
            order.CreatedAt = DateTime.Now;

            if (order.DeliveryTime == null || order.DeliveryTime == 0)
            {
                Random rand = new Random();
                int number = rand.Next(20, 80); //returns random number between 20-80

                //order.DeliveryTime = (double)number+rand.NextDouble(); može i ova verzija
                order.DeliveryTime = (double)number;
            }

            order.Delivered = 0;
            order.DelivererId = 0;
            order.ProductsIDs = orderDTO.ProductsIDs;

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();


            OrderDTO retVal = new OrderDTO();
            retVal = myMapper(order, retVal);

            List<int> productIDs = new List<int>(); //nadjemo koji su to sve proizvodi
            foreach (var x in order.ProductsIDs.Split(','))
            {
                int id = Int32.Parse(x);
                productIDs.Add(id);
            }

            foreach (int id in productIDs)
            {
                ProductDTO p = _mapper.Map<ProductDTO>(_dbContext.Products.Find((long)id));
                retVal.Products.Add(p);
            }

            return retVal;
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

        public OrderDTO UpdateOrder(OrderDTO orderDTO, long id) //ne koristi se pa nije sredjivano za potrebe odbrane
        {
            Order order = _dbContext.Orders.FirstOrDefault(o => o.Id.Equals(id));
            if (order != null)
            {
                order.Address = orderDTO.Address;
                order.Comment = orderDTO.Comment;
                order.TotalPrice = orderDTO.TotalPrice;
                order.DeliveryTime = orderDTO.DeliveryTime;
                order.UsersId = orderDTO.UsersId;
                //order.Products = orderDTO.Products;
                _dbContext.SaveChanges();
            }
            return _mapper.Map<OrderDTO>(order);
        }

        public List<OrderDTO> GetAllOrdersOfUser(long userId)
        {
            List<Order> orders = _dbContext.Orders.Where(x => x.UsersId == userId).ToList();
            List<OrderDTO> orderDTOs = new List<OrderDTO>();
            foreach (Order o in orders)
            {
                OrderDTO orderDTO = new OrderDTO();
                orderDTO = myMapper(o, orderDTO); //napravimo kpiju ordera koja sadrzi proizvode

                List<int> productIDs = new List<int>(); //nadjemo koji su to sve proizvodi
                foreach (var x in o.ProductsIDs.Split(','))
                {
                    int id = Int32.Parse(x);
                    productIDs.Add(id);
                }

                foreach (int id in productIDs)
                {
                    ProductDTO p = _mapper.Map<ProductDTO>(_dbContext.Products.Find((long)id));
                    orderDTO.Products.Add(p);
                }
                orderDTOs.Add(orderDTO);
            }
            return orderDTOs;

        }

        public List<OrderDTO> GetOrderBids()
        {
            List<Order> orders = _dbContext.Orders.Where(x => x.DelivererId.Equals(0) && x.Delivered.Equals(0)).ToList();
            List<OrderDTO> orderDTOs = new List<OrderDTO>();
            foreach (Order o in orders)
            {
                OrderDTO orderDTO = new OrderDTO();
                orderDTO = myMapper(o, orderDTO); //napravimo kpiju ordera koja sadrzi proizvode

                List<int> productIDs = new List<int>(); //nadjemo koji su to sve proizvodi
                foreach (var x in o.ProductsIDs.Split(','))
                {
                    int id = Int32.Parse(x);
                    productIDs.Add(id);
                }

                foreach (int id in productIDs)
                {
                    ProductDTO p = _mapper.Map<ProductDTO>(_dbContext.Products.Find((long)id));
                    orderDTO.Products.Add(p);
                }
                orderDTOs.Add(orderDTO);
            }
            return orderDTOs;
        }

        public OrderDTO TakeOrder(long orderId, long delivererId) 
        {
            var undeliveredOrder = _dbContext.Orders.FirstOrDefault(x => x.DelivererId.Equals(delivererId) && x.Delivered.Equals(0));
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
            var order = _dbContext.Orders.FirstOrDefault(o => o.Id.Equals(orderId));
            if (order != null)
            {
                order.Delivered = 1;
            }
            _dbContext.SaveChanges();
            return _mapper.Map<OrderDTO>(order);
        }

        public List<OrderDTO> GetDeliverersDoneOrders(long delivererId)
        {
            List<Order> orders = _dbContext.Orders.Where(x => x.DelivererId.Equals(delivererId) && x.Delivered.Equals(1)).ToList();
            List<OrderDTO> orderDTOs = new List<OrderDTO>();
            foreach (Order o in orders)
            {
                OrderDTO orderDTO = new OrderDTO();
                orderDTO = myMapper(o, orderDTO); //napravimo kpiju ordera koja sadrzi proizvode

                List<int> productIDs = new List<int>(); //nadjemo koji su to sve proizvodi
                foreach (var x in o.ProductsIDs.Split(','))
                {
                    int id = Int32.Parse(x);
                    productIDs.Add(id);
                }

                foreach (int id in productIDs)
                {
                    ProductDTO p = _mapper.Map<ProductDTO>(_dbContext.Products.Find((long)id));
                    orderDTO.Products.Add(p);
                }
                orderDTOs.Add(orderDTO);
            }
            return orderDTOs;

        }

        public OrderDTO GetDeliverersCurentOrder(long deliverersId)
        {
            Order order = _dbContext.Orders.FirstOrDefault(x => x.DelivererId.Equals(deliverersId) && x.Delivered.Equals(0));
            if (order == null)
                return null;
            OrderDTO retVal = new OrderDTO();
            retVal = myMapper(order, retVal);

            List<int> productIDs = new List<int>(); //nadjemo koji su to sve proizvodi
            foreach (var x in order.ProductsIDs.Split(','))
            {
                int id2 = Int32.Parse(x);
                productIDs.Add(id2);
            }

            foreach (int id2 in productIDs)
            {
                ProductDTO p = _mapper.Map<ProductDTO>(_dbContext.Products.Find((long)id2));
                retVal.Products.Add(p);
            }

            return retVal;

        }

        public OrderDTO GetConsumersCurentOrder(long consumersId)
        {
            Order order = _dbContext.Orders.FirstOrDefault(x => x.UsersId.Equals(consumersId) && x.Delivered.Equals(0));
            if (order == null)
                return null;
            OrderDTO retVal = new OrderDTO();
            retVal = myMapper(order, retVal);

            List<int> productIDs = new List<int>(); //nadjemo koji su to sve proizvodi
            foreach (var x in order.ProductsIDs.Split(','))
            {
                int id2 = Int32.Parse(x);
                productIDs.Add(id2);
            }

            foreach (int id2 in productIDs)
            {
                ProductDTO p = _mapper.Map<ProductDTO>(_dbContext.Products.Find((long)id2));
                retVal.Products.Add(p);
            }

            return retVal;
        }

        public static OrderDTO myMapper(Order old, OrderDTO result)
        {
            result.Id = old.Id;
            result.UsersId = old.UsersId;
            result.Address = old.Address;
            result.Comment = old.Comment;
            result.TotalPrice = old.TotalPrice;
            result.DeliveryTime = old.DeliveryTime;
            result.CreatedAt = old.CreatedAt;
            result.Delivered = old.Delivered;
            result.DelivererId = old.DelivererId;
            //result.User = old.User;

            return result;
        }



    }
}
