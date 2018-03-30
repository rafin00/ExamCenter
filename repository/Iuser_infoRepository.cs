using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repository
{
    interface Iuser_infoRepository
    {
       List<user_info> GetAll();


       int Insert(user_info product);


    }
}
