using System.Web.Mvc;
using Payment_AuthorizeNet_MVC.Models;
using Payment_AuthorizeNet_MVC.Services;

namespace Payment_AuthorizeNet_MVC.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment/Payment
        [HttpGet]
        public ActionResult Payment()
        {
            return View();
        }

        // POST: Payment/Payment
        [HttpPost]
        public ActionResult Payment(PaymentVM paymentVM)
        {
            if (ModelState.IsValid)
            {
                var authNetAccountInfo = new AuthorizeNetAccount()
                {
                    AuthNetLoginID = "82P856deAj",
                    AuthNetTransKey = "48PbEFXx332B2qL3",
                    IsTestMode = true
                };
                var response = AuthorizeCreditCard.Payment(authNetAccountInfo, paymentVM);
                return View("PaymentResponse",response);
            }
            return View();
        }
    }
}
