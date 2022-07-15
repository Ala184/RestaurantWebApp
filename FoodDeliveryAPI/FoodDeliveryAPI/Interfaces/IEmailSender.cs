namespace FoodDeliveryAPI.Interfaces
{
    public interface IEmailSender
    {
        void SendMail(string subject, string text, string usersEmail);
    }
}
