using DBService;
using Model;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MyWeb.Modules
{
    public class HomeModule:NancyModule
    {
        public HomeModule() 
        {
            int pagesize = 5;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            Get["/"] = P =>
            {
               
                dic["ip"] = CommonHelper.GetIPAddress(Request.UserHostAddress);          
                dic["nav"]= CreatNavHtml(1, pagesize);
                IEnumerable<Weblog> list = WeBlogService.GetPageWeblog(1, pagesize);               
                dic["data"] = list;
               
                return View["Home", dic];
            };

            Get["/page_{pageindex}"] = p => 
            {
                IEnumerable<Weblog> list = WeBlogService.GetPageWeblog(p.pageindex, pagesize);
                dic["ip"] = CommonHelper.GetIPAddress(Request.UserHostAddress);
                dic["data"] = list;
                dic["nav"] = CreatNavHtml(p.pageindex, pagesize);
                
                return View["Home", dic];
               
            };

            Get["/Error"] = p =>
            {
                return View["Error"];

            };
           
        }

        public string CreatNavHtml(int pageindex,int pagesize)
        { 
           
            double allcount = WeBlogService.GetAllCount();
            double allpage = Math.Ceiling(allcount / pagesize);
            StringBuilder sb = new StringBuilder();
            string disable=pageindex==1?"class='disabled'":"";
            string clickstr = pageindex == 1 ? "onclick='return false;'" : "";
            sb.Append(string.Format(@"<li {0}>
                          <a href='/page_{1}' {2} aria-label='Previous'>
                              <span aria-hidden='true'>&laquo;</span>
                          </a>
                        </li>", disable,pageindex-1,clickstr));
            for (int i = 1; i <= allpage; i++)
            {
                string curpage = pageindex == i ? "class='active'" : "";
                sb.Append(" <li " + curpage + "><a href='/page_" + i + "'>" + i + "</a></li>");
            }
            disable = pageindex == allpage ? "class='disabled'" : "";
            clickstr = pageindex == allpage ? "onclick='return false;'" : "";
            sb.Append(string.Format(@"<li {0}>
                          <a href='/page_{1}' {2} aria-label='Next'>
                              <span aria-hidden='true'>&raquo;</span>
                          </a>
                        </li>", disable,pageindex+1,clickstr));
            return sb.ToString();
        }
    }
}