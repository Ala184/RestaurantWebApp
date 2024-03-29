﻿using FoodDeliveryAPI.DTO;

namespace FoodDeliveryAPI.Interfaces
{
    public interface IOrderService
    {
        List<OrderDTO> GetAllOrders();


        void DeleteOrder(long id);

        OrderDTO GetOrderById(long id);

        List<OrderDTO> GetAllOrdersOfUser(long userId);
        List<OrderDTO> GetOrderBids();

        List<OrderDTO> GetDeliverersDoneOrders(long delivererId);
        OrderDTO GetDeliverersCurentOrder(long deliverersId);
        OrderDTO GetConsumersCurentOrder(long consumersId);

        OrderDTO UpdateOrder(OrderDTO orderDTO, long id);


        OrderDTO AddNewOrder(NewOrderDTO orderDTO);
        OrderDTO TakeOrder(long orderId, long delivererId);

        OrderDTO OrderIsDelivered(long orderId);

    }
}
