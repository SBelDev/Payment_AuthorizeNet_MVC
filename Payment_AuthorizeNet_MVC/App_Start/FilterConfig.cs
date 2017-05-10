using System.Web;
using System.Web.Mvc;

namespace Payment_AuthorizeNet_MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
