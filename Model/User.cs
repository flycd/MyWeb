using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
   public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string LoginName { get; set; }
        public string Pwd { get; set; }
    }
}
