using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository
{
   public class resultRepository
   {
       excenterdbContext context = new excenterdbContext();
       public List<result> GetAllReport()
       {
           string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
           SqlConnection con = new SqlConnection(connStr);
           string sql = "SELECT course_id, avg(marks) as mark FROM result GROUP BY course_id";
           
           SqlCommand cmd = new SqlCommand(sql, con);
           con.Open();

           SqlDataReader reader = cmd.ExecuteReader();

           List<result> rlist = new List<result>();

           while (reader.Read())
           {
               result r = new result();
               r=context.results.ToList().Find(res=>res.course_id==Convert.ToInt32(reader["course_id"]));
               
              
               r.marks =Convert.ToInt32(reader["mark"]);
               
               rlist.Add(r);
           }

           return rlist;
       }

       public List<result> GetAllReport(string user_name)
       {
           string connStr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
           SqlConnection con = new SqlConnection(connStr);
           string sql = "SELECT course_id, avg(marks) as mark FROM (select * from result where user_name='"+user_name+"' ) as result GROUP BY course_id ";

           SqlCommand cmd = new SqlCommand(sql, con);
           con.Open();

           SqlDataReader reader = cmd.ExecuteReader();

           List<result> rlist = new List<result>();

           while (reader.Read())
           {
               result r = new result();
               r = context.results.ToList().Find(res => res.course_id == Convert.ToInt32(reader["course_id"]));


               r.marks = Convert.ToInt32(reader["mark"]);

               rlist.Add(r);
           }

           return rlist;
       }

    }
}
