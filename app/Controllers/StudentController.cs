using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using repository;

namespace app.Controllers
{
    public class StudentController : BaseController
    {

        courseRepository crsrepo = new courseRepository();
        questionRepository quesrepo = new questionRepository();
        evntRepository evrepo = new evntRepository();
        examRepository exrepo = new examRepository();
        user_infoRepository usrepo = new user_infoRepository();
        registrationRepository regisrepo = new registrationRepository();
        resultRepository resrepo = new resultRepository();
        AnswersRepository ansrepo = new AnswersRepository();

        public ActionResult Index()
        {

            return View(regisrepo.GetStdEvnt(Session["user_name"].ToString()));
        }
        [HttpGet]
        public ActionResult Start(int evnt_id)
        {
            if (evrepo.Get(evnt_id).evnt_sdt > DateTime.Now)
            {
                TempData["error"] = "Wait till due time";
                return RedirectToAction("Index");

            }
            else if (evrepo.Get(evnt_id).evnt_edt < DateTime.Now)
            {
                return RedirectToAction("Index");
            }
            else
            {

                return RedirectToAction("Start1", new { evnt_id, course_id = exrepo.GetByEvnt(evnt_id).ElementAt(0).course_id });
            }

        }
        [HttpGet]
        public ActionResult Start1(int evnt_id, int course_id)
        {
            ViewBag.answers = ansrepo.GetAnswers(evnt_id, Session["user_name"].ToString());
            ViewBag.courses = exrepo.GetByEvnt(evnt_id);
            return View(exrepo.GetquesByEvntCourse(evnt_id,course_id));
        }
        [HttpPost]
        public ActionResult Start1()
        {
            int evnt_id =Convert.ToInt32(Request.QueryString["evnt_id"]);
            regisrepo.calculateResult(evnt_id, Session["user_name"].ToString());
            return RedirectToAction("Index");
            
        }

        public void SaveAnswers(int id,string answer,int para,int crs)
        {
            string user_name = Session["user_name"].ToString();
            ansrepo.saveAnswer(para, crs, user_name, id, answer);

            
        }
        [HttpGet]
        public ActionResult ViewAllResults()
        {

            return View(regisrepo.GetAllResults(Session["user_name"].ToString()));
        }
        [HttpGet]
        public ActionResult ViewReport()
        {
            return View(resrepo.GetAllReport(Session["user_name"].ToString()));
        }

    }
}
