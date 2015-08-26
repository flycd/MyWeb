using LiteDB;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBService
{
    public class UserService
    {



        public static void AddWeblog(User model)
       {
           using (var db = DbBase.CreateDBContext())
           {
               var list = db.GetCollection<User>("users");
               list.Insert(model);
               
               
           }
       }

        public static IEnumerable<User> GetAllWeblog()
       {
           var db = DbBase.CreateDBContext();
           var list = db.GetCollection<User>("users");
               return list.FindAll();
               
       }

        public static User GetWeblogById(int id)
       {
           var db = DbBase.CreateDBContext();
           var list = db.GetCollection<User>("users");
               return list.FindById(id);


           
       }
    }
}
