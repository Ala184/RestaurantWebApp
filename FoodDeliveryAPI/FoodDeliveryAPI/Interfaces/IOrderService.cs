using FoodDeliveryAPI.DTO;

namespace FoodDeliveryAPI.Interfaces
{
    public interface IOrderService
    {
        List<OrderDTO> GetAllOrders();

        OrderDTO AddNewOrder(OrderDTO orderDTO);

        void DeleteOrder(long id);

        OrderDTO GetOrderById(long id);

        List<OrderDTO> GetAllOrdersOfUser(long userId);
        List<OrderDTO> GetOrderBids();

        List<OrderDTO> GetDeliverersDoneOrders(long delivererId);
        OrderDTO GetDeliverersCurentOrder(long deliverersId);
        OrderDTO GetConsumersCurentOrder(long consumersId);

        OrderDTO UpdateOrder(OrderDTO orderDTO, long id);


        OrderDTO TakeOrder(long orderId, long delivererId);

        OrderDTO OrderIsDelivered(long orderId);

    }
}
