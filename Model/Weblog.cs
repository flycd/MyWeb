using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Weblog
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public string  Writer { get; set; }
        public string BlogType { get; set; }
        public string BlogLabel { get; set; }
    }
}
