namespace Payment_AuthorizeNet_MVC.Models
{
    public class PaymentResponseVM
    {
        public string AuthorizationCode { get; set; }
        public string TransactionID { get; set; }
        public string ResponseCode { get; set; }
        public string MessagesCode { get; set; }
        public string MessageDescription { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}