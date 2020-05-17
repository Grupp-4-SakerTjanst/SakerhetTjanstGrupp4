using System.Web;
using System.Web.Mvc;

namespace SakerhetTjanstGrupp4
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
