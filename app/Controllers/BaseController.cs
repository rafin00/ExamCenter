using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace app.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/



        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["user_name"] == null)
            {
                Response.Redirect("/Login");
            }
            
        }      

    }
}
