using repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace app.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        user_infoRepository repo = new user_infoRepository();
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["user_name"]==null)
            return View();
            else
               return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        public ActionResult Index(user_info us)
        {
            if (repo.Get(us.user_name) != null)
            {
                us = repo.Get(us.user_name);
                Session["user_name"] = us.user_name; Session["user_type"] = us.user_type;
                Session["name"] = us.name;
                if (us.user_type == "admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (us.user_type == "teacher")
                    return RedirectToAction("Index", "Teacher");
                else
                    return RedirectToAction("Index", "Student");
            }
            else
            {
                ViewBag.error = "Invalid Username/Password";
                return View();
            }
        }
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(user_info us)
        {
            if (repo.checkUsername(us.user_name))
            {
                ViewBag.error = "User_name Exists!!";
                return View();
            }
            else
            {
                if (us.name != "" && us.password != null)
                {
                    us.user_type = "student";
                    repo.Insert(us);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Please Fill All Fields";
                    return View();
                }
            }
        }

    }
}
