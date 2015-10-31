using leishi.Web.Core;
using System.Web.Mvc;

namespace leishi.Web.Controllers
{
    public class HomeController : CoreController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}