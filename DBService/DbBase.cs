using LiteDB;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DBService
{
   public class DbBase
    {
       
       public static LiteDatabase CreateDBContext()
       {
           //打开或者创建新的数据库

           var db = new LiteDatabase("App_Data/MyData.db");
           return db;
       }
      
      
    }
}
