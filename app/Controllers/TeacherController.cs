
using repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace app.Controllers
{
    public class TeacherController : BaseController
    {
        
       
        courseRepository crsrepo = new courseRepository();
        questionRepository quesrepo = new questionRepository();
        evntRepository evrepo = new evntRepository();
        examRepository exrepo = new examRepository();
        user_infoRepository usrepo = new user_infoRepository();
        registrationRepository regisrepo = new registrationRepository();
        resultRepository resrepo = new resultRepository();



        public ActionResult Index()
        {
            return View(evrepo.GetAllComing());
        }
       
        [HttpGet]
        public ActionResult AddNewCourse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewCourse(string courseName)
        {
            if (courseName == "") { ViewBag.error="Please Fill All feilds";
             return View(); }
            else
            {
                course crs = new course();
                crs.course_name = courseName;
                crsrepo.AddNewCourse(crs);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult ViewQuestions(int course_id,string course_name)
        {
            
            return View(crsrepo.Get(course_id));
        }

        [HttpGet]
        public ActionResult AddNewQuestions(int course_id)
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddNewQuestions(question ques)
        {
            if (ques.question_text == null || ques.option1 == null || ques.option2 == null)
            {
                ViewBag.error = "Please Fill All fields";
                return View();
            }
            else
            {
                if (ques.answer == "1")
                    ques.answer = ques.option1;
                else
                    ques.answer = ques.option2;
                quesrepo.Insert(ques);
                return RedirectToAction("ViewQuestions", new { course_id = ques.course_id });
            }
        }
        [HttpGet]
         public ActionResult ViewEvnt(int evnt_id,string evnt_name)
         {
            
            
            return View(crsrepo.GetByExam(exrepo.GetByEvnt(evnt_id)));
         }

        [HttpGet]
        public ActionResult ViewAllEvnt()
        {


            return View(evrepo.GetAll());
        }

        [HttpGet]
        public ActionResult AddNewEvnt()
        {
            return View();
        }

        [HttpPost,ActionName("AddNewEvnt")]
        public ActionResult AddNewEvntPost(evnt ev)
        {
            if (ev.evnt_name == "" || ev.evnt_sdt == null || ev.evnt_edt == null)
            {
                ViewBag.error = "Please Fill all fields";
                return View();
            }
            else
            {
                evrepo.Insert(ev);
                return RedirectToAction("ViewEvnt", new { evnt_id = ev.evnt_id,evnt_name=ev.evnt_name });
            }
        }

        [HttpGet]
        public ActionResult AddEvntCourse(int evnt_id)
        {

            return View(crsrepo.GetAll());
        }

        [HttpPost]
        public ActionResult AddEvntCourse(int course_id, int totalQuestions, int questionSelection,int evnt_id)
        {
            if (totalQuestions == 0||totalQuestions==null) { ViewBag.error = "Total Questions Can Not be Zero"; return View(); }
            else if (!quesrepo.checkTotalQuestions(course_id, totalQuestions))
            {
                ViewBag.error = "Less quesion in repositry";
                return RedirectToAction("AddEvntCourse", new {evnt_id });
            }
            else
            {
                if (questionSelection == 1)
                {
                    exrepo.AddQuestionsRandomly(course_id, totalQuestions, evnt_id);
                    return RedirectToAction("ViewEvnt", new { evnt_id });


                }
                else
                {

                    return RedirectToAction("SelectQuestions", new { evnt_id, totalQuestions, course_id });
                }
            }
        }

        [HttpGet]
        public ActionResult SelectQuestions(int evnt_id, int totalQuestions, int course_id)
        {   
            ViewBag.check = exrepo.GetByEvnt2(Convert.ToInt32(TempData["evnt_id"]));
            return View(quesrepo.GetAllByCourse(course_id));
        }
        [HttpPost]
        public ActionResult SelectQuestions(IEnumerable<int> values)
        {
          
            exrepo.AddQuestionsManually(values,Convert.ToInt32(TempData["course_id"]),Convert.ToInt32(TempData["evnt_id"]));

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AssignStudent(int evnt_id)
        {
            ViewBag.check=regisrepo.GetAllByEvnt(evnt_id);
            return View(usrepo.GetAllByUser("student"));
        }

        [HttpPost,ActionName("AssignStudent")]
        public ActionResult AssignStudentPost( IEnumerable<string> values)
        {

           List<string> val = values.ToList();
           int evnt_id = Convert.ToInt32(TempData["evnt_id"].ToString());
           List<user_info> allStd= usrepo.GetAllByUser("student");
            foreach(string std in val)
            {
                allStd.RemoveAll(us => us.user_name == std);
                
                
            }
           

           regisrepo.Register(val,allStd, evnt_id);
           RedirectToAction("index");
           return RedirectToAction("index");
        }
        [HttpGet]
        public ActionResult ViewEvntQuestions(int evnt_id, int course_id,string evnt_name)
        {
            return View(exrepo.GetEvntQuesByCourse(evnt_id,course_id));
        }
        [HttpGet]
        public ActionResult ViewCourses()
        {
            return View(crsrepo.GetAll());
        }
        [HttpGet]
        public ActionResult EditAllCourseQuestions(int course_id, string course_name)
        {
            return View(quesrepo.GetAllByCourse(course_id));
        }
        [HttpGet]
        public ActionResult EditQuestion(int question_id,string course_name)
        {
            
            return View(quesrepo.GetById(question_id));
        }
        [HttpPost]
        public ActionResult EditQuestion(question item)
        {
            quesrepo.UpdateByObject(item);
            return RedirectToAction("ViewQuestions", new { course_id=item.course_id, course_name=Request.QueryString["course_name"] });
        }
        [HttpGet]
        public ActionResult ViewResults()
        {
            return View(evrepo.GetAllPast());
        }
        [HttpGet]
        public ActionResult ViewEvntResult(int evnt_id)
        {

            TempData["Names"] = usrepo.GetAllByEvnt(evnt_id);
            return View(regisrepo.GetResults(evnt_id));
        }
        [HttpGet]
        public ActionResult ViewReport()
        {

            return View(resrepo.GetAllReport());
        }
    }
}
