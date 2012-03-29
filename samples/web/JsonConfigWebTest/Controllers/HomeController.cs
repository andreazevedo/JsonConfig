using System.Text;
using System.Web.Mvc;
using JsonConfig;

namespace JsonConfigWebTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var sb = new StringBuilder();
            sb.Append("name: " + JsonConfigManager.DefaultConfig.name + "<br />");
            sb.Append("host: " + JsonConfigManager.DefaultConfig.host + "<br />");
            sb.Append("port: " + JsonConfigManager.DefaultConfig.port + "<br />");

            return Content(sb.ToString());
        }

    }
}
