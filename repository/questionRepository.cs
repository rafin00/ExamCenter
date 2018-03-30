using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository
{
   public class questionRepository
    {
       excenterdbContext context = new excenterdbContext();
       public int Insert(question ques)
       {
           context.questions.Add(ques);
           return context.SaveChanges();
       }
       public List<question> GetAllByCourse(int course_id)
       {
          return context.questions.ToList().FindAll(ques => ques.course_id == course_id);
       }
       public void DeleteByObject(question ques)
       {
           context.questions.Remove(ques);
       }

       public void UpdateByObject(question ques)
       {
          context.questions.Remove(context.questions.ToList().Find(q=>q.question_id==ques.question_id));
          context.questions.Add(ques);
          context.SaveChanges();
       }
       public question GetById(int question_id)
       {
           return context.questions.ToList().Find(q=>q.question_id==question_id);
       }

       public bool checkTotalQuestions(int course_id, int totalQuestions)
       {
           if (context.questions.ToList().FindAll(ques => ques.course_id == course_id).Count() < totalQuestions)
           {
               return false;
           }
           else
               return true;
       }


    }
}
