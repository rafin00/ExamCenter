using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository
{
   public class evntRepository
    {
       excenterdbContext context = new excenterdbContext();

       public List<evnt> GetAll()
       {
           return context.evnts.ToList();
       }
       public int Insert(evnt ev)
       {
           
           ev.evnt_id=context.evnts.Select(e => e.evnt_id).Max()+1;
           context.evnts.Add(ev);
           return context.SaveChanges();
       }
       public List<evnt> GetAllComing()
       {
           object a = DateTime.Today;
           return context.evnts.ToList().FindAll(ev=>ev.evnt_edt>DateTime.Now);

       }

       public List<evnt> GetAllPast()
       {
           return context.evnts.ToList().FindAll(ev => ev.evnt_edt < DateTime.Now);
       }
       //////////////////////////
       //studnt
       ///////////////////////
       public evnt Get(int evnt_id)
       {
           return context.evnts.ToList().Find(ev => ev.evnt_id == evnt_id);
       }

    }
}
