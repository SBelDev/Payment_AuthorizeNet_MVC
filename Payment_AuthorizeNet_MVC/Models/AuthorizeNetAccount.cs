namespace Payment_AuthorizeNet_MVC.Models
{
    public class AuthorizeNetAccount
    {
        public string AuthNetLoginID { get; set; }
        public string AuthNetPassword { get; set; }
        public string AuthNetTransKey { get; set; }
        public bool IsTestMode { get; set; }
    }
}