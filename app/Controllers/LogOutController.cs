using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace app.Controllers
{
    public class LogOutController : Controller
    {
        //
        // GET: /LogOut/

        public ActionResult Index()
        {
            Session["user_name"] = null; Session["user_type"] = null; Session["name"] = null;
            return RedirectToAction("Index","Login");
        }

    }
}
