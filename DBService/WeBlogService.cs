using LiteDB;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBService
{
    public class WeBlogService
    {
       
      /// <summary>
      /// 添加
      /// </summary>
      /// <param name="model"></param>
       public static void AddWeblog(Weblog model)
       {
           using (var db = DbBase.CreateDBContext())
           {
               var list = db.GetCollection<Weblog>("weblogs");
               list.Insert(model);
             
               
           }
       }

        /// <summary>
        /// 获取全部内容
        /// </summary>
        /// <returns></returns>
       public static IEnumerable<Weblog> GetAllWeblog()
       {
           var db = DbBase.CreateDBContext();
               var list = db.GetCollection<Weblog>("weblogs");
               return list.Find(Query.All("CreateTime", Query.Descending));
           
       }

        /// <summary>
        /// 获取分页内容
        /// </summary>
        /// <param name="pageindex">当前页</param>
        /// <param name="pagesize">每页几条</param>
        /// <returns></returns>
       public static IEnumerable<Weblog> GetPageWeblog(int pageindex,int pagesize)
       {
           var db = DbBase.CreateDBContext();
           var list = db.GetCollection<Weblog>("weblogs");
           int skip=(pageindex-1)*pagesize;

           return list.Find(Query.All("CreateTime",Query.Descending), skip, pagesize);

       }

       public static Weblog GetWeblogById(int id)
       {
           var db = DbBase.CreateDBContext();
               var list = db.GetCollection<Weblog>("weblogs");
               return list.FindById(id);


           
       }

       public static void Delete(int id)
       {
           var db = DbBase.CreateDBContext();
           var list = db.GetCollection<Weblog>("weblogs");
           list.Delete(id);
       }

       public static int GetAllCount()
       {
           var db = DbBase.CreateDBContext();
           var list = db.GetCollection<Weblog>("weblogs");
           return  list.Count();
       }

       public static bool Update(Weblog model)
       {
           var db = DbBase.CreateDBContext();
           var list = db.GetCollection<Weblog>("weblogs");
          return list.Update(model);
       }
    }
}
