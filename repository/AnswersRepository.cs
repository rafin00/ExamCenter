using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository
{
   public class AnswersRepository
    {
        excenterdbContext context = new excenterdbContext();

        public void saveAnswer(int evnt_id, int course_id, string user_name, int question_id, string myans)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection con = new SqlConnection(connStr);
        


            Answer ans = new Answer();
            ans.course_id = course_id;
            ans.evnt_id = evnt_id;
            ans.question_id = question_id;
            ans.user_name = user_name;
            ans.myans = myans;
            Answer preans = context.Answers.ToList().Find(a => a.question_id == ans.question_id && a.evnt_id == ans.evnt_id && a.user_name == ans.user_name);

            if (preans == null)
            {
               
               
                context.Answers.Add(ans);
                int a= context.SaveChanges();
            }
            else if(preans.myans!=ans.myans)
            {
                string sql = "update Answers set myans='"+myans+"' where user_name='"+user_name+"' and question_id="+question_id+" and evnt_id="+evnt_id+"";
                SqlCommand cmd = new SqlCommand(sql, con);
                if (con.State != ConnectionState.Open) con.Open();
                cmd.ExecuteNonQuery();
              
            }


        }

        public List<Answer> GetAnswers(int evnt_id,string user_name)
        {
           return context.Answers.ToList().FindAll(a=>a.evnt_id==evnt_id && a.user_name==user_name);
        }

    }
}
