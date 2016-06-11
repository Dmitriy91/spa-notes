using System.Web.Mvc;

namespace SpaNotes.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Views/Index.cshtml");
        }
    }
}
