using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository
{
    public class user_infoRepository : Iuser_infoRepository
    {
        excenterdbContext context = new excenterdbContext();
        public List<user_info> GetAll()
        {
            return context.user_info.ToList();
        }

        public user_info Get(string user_name)
        {
            return context.user_info.SingleOrDefault(us=>us.user_name==user_name);
        }

        public List<user_info> GetAllByUser(string user_type)
        {
            return context.user_info.ToList().FindAll(us => us.user_type == user_type);
        }

        public int Insert(user_info us)
        {
            context.user_info.Add(us);
            return context.SaveChanges();
        }
        public int Update(user_info user)
        {
            user_info userToUpdate = context.user_info.SingleOrDefault(us => us.user_name == user.user_name);
            userToUpdate.name = user.name;
            userToUpdate.user_type = user.user_type;
            userToUpdate.password = user.password;

            return context.SaveChanges(); 
        }
        public int Delete(string user_name)
        {
            user_info user = context.user_info.SingleOrDefault(us => us.user_name == user_name);
            context.user_info.Remove(user);
            return context.SaveChanges();
        }
        public List<user_info> GetAllByEvnt(int evnt_id)
        {
           List<registration> reg= context.registrations.ToList().FindAll(re => re.evnt_id == evnt_id);
           List<user_info> users = new List<user_info>();
           foreach (registration r in reg)
           {
               user_info user = context.user_info.ToList().Find(us=>us.user_name==r.user_name);
               users.Add(user);

           }
           return users;
        }
        public bool checkUsername(string user_name)
        {
           if(context.user_info.ToList().Find(us=>us.user_name==user_name)!=null)
           { return true; }
           else return false;

        }
    }
}
