using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository
{
   public class courseRepository
    {
        excenterdbContext context = new excenterdbContext();

        public List<course> GetAll()
        {
            return context.courses.ToList();
        }
        public List<question> Get(int course_id)
        {
            return context.questions.ToList().FindAll(ques => ques.course_id == course_id);
        }

        public course GetSingle(int course_id)
        {
            return context.courses.SingleOrDefault(crs => crs.course_id == course_id);
        }

        public int AddNewCourse(course crs)
        {
             context.courses.Add(crs);
             return context.SaveChanges();
        }
        public List<course> GetByExam(List<exam> ex)
        {
            List<course> crs=new List<course>();
            foreach(exam exm in ex)
            {
                crs.Add(GetSingle(exm.course_id));
            }
            return crs;
        }
    }
}
