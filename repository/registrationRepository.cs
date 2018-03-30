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
    public class registrationRepository
    {
        excenterdbContext context = new excenterdbContext();
        string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public List<registration> GetAll(string user_name)
        {
            return context.registrations.ToList().FindAll(ev => ev.user_name == user_name);
        }
        public int Register(List<string> user_name,List<user_info> unreg, int evnt_id)
       {
           foreach (string us in user_name)
           {

              registration regg= context.registrations.ToList().Find(reg => (reg.user_name == us)&&(reg.evnt_id==evnt_id));
               if(regg==null)
                    {
                   regg=new registration();
                   regg.user_name=us;
                   regg.evnt_id=evnt_id;
                   regg.result=-1;
                   context.registrations.Add(regg);
                   context.SaveChanges();
                 }
           }

           foreach (user_info std in unreg)
           {

               registration regg = context.registrations.ToList().Find(reg => (reg.user_name == std.user_name)&&(reg.evnt_id==evnt_id));
               if (regg == null)
               {
                   
               }
               else
               {
                   context.registrations.Remove(context.registrations.ToList().Find(reg => (reg.user_name == std.user_name)&&(reg.evnt_id==evnt_id)));
                   context.SaveChanges();
               }
               
           }


           return 1;
       }
        public List<registration> GetAllByEvnt(int evnt_id)
        {
            return context.registrations.ToList().FindAll(reg => reg.evnt_id == evnt_id);
        }

        public List<registration> GetResults(int evnt_id)
        {
            return context.registrations.ToList().FindAll(re=>re.evnt_id==evnt_id);
        }
        /////////////////////////////
        //student
        //////////////////////////

        public List<registration> GetStdEvnt(string user_name)
        {
          return  context.registrations.ToList().FindAll(reg => reg.user_name == user_name && reg.result == -1 && reg.evnt.evnt_edt>DateTime.Now );
        }

        public void calculateResult(int evnt_id, string user_name)
        {
            SqlConnection con = new SqlConnection(connStr);
            string sql = "select Answers.myans, answers.question_id,question.answer, question.course_id from (select * from answers where user_name='"+user_name+"' and evnt_id='"+evnt_id+"') as answers inner join question on answers.question_id = question.question_id";


            if (con.State != ConnectionState.Open) con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
           

             SqlDataReader reader = cmd.ExecuteReader();
             

             List<result> res = new List<result>();
             int result = 0;
            while (reader.Read())
            {
                if (reader["myans"].ToString() == reader["answer"].ToString())
                {
                    result++;
                    

                }

                
            }
            reader.Close();

            sql="update registration set result='"+result+"' where user_name='"+user_name+"' and evnt_id='"+evnt_id+"'";
            cmd = new SqlCommand(sql, con); cmd.ExecuteNonQuery();

             sql = "select t1.course_id,t1.total,t2.corr from (select table2.course_id, count(table2.ID) as total from (select * from exam where evnt_id='"+evnt_id+"') as table2 group by table2.course_id) as t1 full outer join ((select table1.course_id, count(table1.myans) as corr from  (select answers.course_id,answers.question_id,answers.myans, question.answer from (select * from Answers where evnt_id='"+evnt_id+"' and user_name='"+user_name+"') as answers inner join question on Answers.question_id=question.question_id)as table1 where table1.myans=table1.answer group by table1.course_id)) as t2 on t1.course_id=t2.course_id" ;
             cmd = new SqlCommand(sql, con);
            
              reader = cmd.ExecuteReader();
              double total = 0; double corr = 0; int course_id; double avg = 0;
            while (reader.Read())
            {
                total = 0; corr = 0; course_id = 0; avg = 0;
                if (reader["course_id"] != DBNull.Value)
                course_id=Convert.ToInt32(reader["course_id"]);
              if (reader["corr"] != DBNull.Value) corr = Convert.ToInt32(reader["corr"]);
              if (reader["total"]!= DBNull.Value) total = Convert.ToInt32(reader["total"]);
              if (total != 0) avg = (corr / total) * 100;

              sql = "insert into result(evnt_id,course_id,user_name,marks) values(" + evnt_id + "," + course_id + ",'" + user_name + "'," + Convert.ToInt32(avg) + ")";
              if (con.State != ConnectionState.Open) con.Open();
              cmd = new SqlCommand(sql, con); cmd.ExecuteNonQuery();
              }
            
        }

        public List<registration> GetAllResults(string user_name)
        {
            return context.registrations.ToList().FindAll(reg => reg.result != -1 && reg.user_name==user_name);
        }


    }
}
