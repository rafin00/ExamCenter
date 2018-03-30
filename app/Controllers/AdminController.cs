using repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace app.Controllers
{
    public class AdminController : BaseController
    {
        
        user_infoRepository repo = new user_infoRepository();
        public ActionResult Index()
        {
            if (Session["user_name"] != null)
                return View(repo.GetAllByUser("teacher"));
            else
                return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(user_info us)
        {
            us.user_type = "teacher";
            repo.Insert(us);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(string user_name)
        {

            return View(repo.Get(user_name));
        }
        [HttpPost]
        public ActionResult Edit(user_info us)
        {
            repo.Update(us);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(string user_name)
        {

            return View(repo.Get(user_name));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ConfirmDelete(string user_name)
        {
            
            repo.Delete(user_name);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult LogOut()
        {

            
            return RedirectToAction("Index","Logout");
        }
        

    }
}
