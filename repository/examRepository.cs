using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace repository
{
   public class examRepository
   {
       excenterdbContext context = new excenterdbContext();
         string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;



         public List<exam> GetByEvnt(int evnt_id)
         {
             List<exam> exl = context.exams.ToList().FindAll(ex => ex.evnt_id == evnt_id)
                              .GroupBy(e => e.course_id).Select(e => e.First()).ToList()
                              ;
             return exl;

         }
         public List<exam> GetByEvnt2(int evnt_id)
         {
             List<exam> exl = context.exams.ToList().FindAll(ex => ex.evnt_id == evnt_id);
             return exl;

         }
       public  void Shuffle<T>( IList<T> list)
       {
           int n = list.Count;
           Random rnd = new Random();
           while (n > 1)
           {
               int k = (rnd.Next(0, n) % n);
               n--;
               T value = list[k];
               list[k] = list[n];
               list[n] = value;
           }
       }
       public int AddQuestionsRandomly(int course_id,int totalQuestions,int event_id)
       {  
           List<question> qToSort= context.questions.ToList().FindAll(ques => ques.course_id == course_id);
           Shuffle(qToSort);

           List<exam> quesToAdd = new List<exam>();
           exam ex = new exam();
           for (int i = 1; i <= totalQuestions; i++)
           {
               ex.course_id = course_id;
               ex.evnt_id = event_id;
               ex.question_set = 'A';
               ex.question_id = qToSort.ElementAt(i).question_id;
               quesToAdd.Add(ex);
               context.exams.Add(ex);
               SqlConnection con = new SqlConnection(connStr);
               string sql = "insert into exam(course_id,evnt_id,question_set,question_id) values('" + course_id + "','"+event_id+"',1,'"+ex.question_id+"')";

               
               if (con.State != ConnectionState.Open) con.Open();
               SqlCommand cmd = new SqlCommand(sql, con);
               cmd.ExecuteNonQuery();
           }
           
           return 1;
       }
       public int AddQuestionsManually(IEnumerable<int> checkedQuestions,int course_id,int evnt_id)
       {
           List<question> allQues = context.questions.ToList().FindAll(ques => ques.course_id == course_id);
           List<question> unCheckedQuestions=allQues;
           foreach (int quesid in checkedQuestions)
           {

               unCheckedQuestions = unCheckedQuestions.FindAll(ques => ques.question_id != quesid);
               
           }

           foreach(int quesid in checkedQuestions)
           {
               if (context.exams.ToList().Find(ex => ex.question_id == quesid && ex.evnt_id==evnt_id) == null)
               {
                   exam exm = new exam();
                   exm.evnt_id = evnt_id;
                   exm.question_id = quesid;
                   exm.course_id = course_id;
                   exm.question_set = 1;
                   context.exams.Add(exm);
                   context.SaveChanges();
               }
           }

           foreach (question ques in unCheckedQuestions)
           {
               if (context.exams.ToList().Find(ex => ex.question_id == ques.question_id && ex.evnt_id == evnt_id) != null)
               {
                   context.exams.Remove(context.exams.ToList().Find(ex => ex.question_id == ques.question_id && ex.evnt_id == evnt_id));
                   context.SaveChanges();
               }
           }

           return 0;
       }

       public List<question> GetEvntQuesByCourse(int evnt_id, int course_id)
       {
           List<exam> e= context.exams.ToList().FindAll(ex => ex.course_id == course_id && ex.evnt_id == evnt_id);
           List<question> ques = new List<question>();
           foreach (exam ex in e)
           {
               question q = context.questions.ToList().Find(que=>que.question_id==ex.question_id);
               ques.Add(q);
           }
           return ques;
       }
       ///////////////

       public List<exam> GetquesByEvntCourse(int evnt_id, int course_id)
       {
           return context.exams.ToList().FindAll(ex => ex.evnt_id == evnt_id && ex.course_id == course_id);
       }

    }
}
