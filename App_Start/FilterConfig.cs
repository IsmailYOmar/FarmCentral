using System.Web;
using System.Web.Mvc;

namespace ST10120867_FarmCentral
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
