using DBService;
using Model;
using Nancy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace MyWeb.Modules
{
    public class BlogModule:NancyModule
    {
        public BlogModule() {


            Dictionary<string, object> dic = new Dictionary<string, object>();
            Get["/Login"] = P =>
            {
                if (Session["user"] != null)
                    return "<script>location.href='/Manage'</script>";
              
               return View["Blog/Login"];
            };

            Post["/Login"] = P =>
             {
                var user= Request.Form;
                if (string.IsNullOrEmpty(user.username.Value.Trim()))
                {
                    return "<script>alert('用户名不能为空！');location.href='/Login'</script>";

                }
                if (string.IsNullOrEmpty(user.pwd.Value.Trim()))
                {
                    return "<script>alert('Password cannot be empty');location.href='/Login'</script>";
                }
                if (user.pwd.Value.Trim() != "1" || user.username.Value.Trim() != "admin")
                {
                    return "<script>alert('Username or Password Error');location.href='/Login'</script>";
                }

                User u = new User() { LoginName = user.username.Value.Trim(), Pwd = user.pwd.Value.Trim() , UserName="陈东"};
                Session["user"] = u;
                return "<script>location.href='/Manage'</script>";
             };

            Get["Manage"] = P =>
            {
                if (Session["user"] == null)
                    return "<script>location.href='/'</script>";

                IEnumerable<Weblog> list = WeBlogService.GetAllWeblog();
                return View["Blog/Manage", list];
            };

            Get["/Blog/Add"] = P =>
            {
                if (Session["user"] == null)
                    return "<script>location.href='/'</script>";

                return View["Blog/Add"];
            };

            Post["/Blog/Add"] = p => 
            {
                var pars = Request.Form;
               
                Weblog model = new Weblog();
                model.Title = pars.title.Value.Trim();
                model.Writer = pars.writer.Value.Trim();
                model.Content = pars.content.Value.Trim();
                model.CreateTime = DateTime.Now;
                WeBlogService.AddWeblog(model);
                return "<script>alert('ok');location.href='/'</script>";
            };

            Get["/Delete/{id}"]=p=>
            {
                WeBlogService.Delete(p.id);
                return "<script>alert('ok');location.href='/Manage'</script>";
            };

            Get["/detial_{id}"] = p =>
            {
                Weblog model = WeBlogService.GetWeblogById(p.id);
                dic["blog"] = model;
                dic["ip"] = CommonHelper.GetIPAddress(Request.UserHostAddress);
                return View["Blog/Detial",dic];
            };

            Get["/edit/{id}"] = p => 
            {
                Weblog model = WeBlogService.GetWeblogById(p.id);
               
                return View["blog/Edit",model];
            };

            Post["/Blog/Edit"] = p =>
                {
                    var pars = Request.Form;

                    Weblog model = WeBlogService.GetWeblogById(int.Parse(pars.ID.Value));
                    model.Title = pars.title.Value.Trim();
                    model.Writer = pars.writer.Value.Trim();
                    model.Content = pars.content.Value.Trim();
                    
                    WeBlogService.Update(model);

                    return "<script>alert('ok');location.href='/Manage'</script>";
                };


            Post["/Blog/Uploadimg"] = p => {

                
                //获取文件对象 （必须用 'wangEditor_uploadImg' 获取图片文件 ）["wangEditor_uploadImg"]
                IEnumerable<HttpFile> Files = Request.Files;
                var stream = Files.First().Value;
                var filename = Files.First().Name;
                var arr = filename.Split('\\');
                var realname = arr[arr.Length-1];
                var extarr = realname.Split('.');
                var ext = extarr[extarr.Length - 1];
                var newname = DateTime.Now.Ticks.ToString() + "." + ext;


                using (FileStream fs = new FileStream("UploadImg/" + newname, FileMode.Create))
                {
                    byte[] bytes = new byte[stream.Length];
                    int numBytesRead = 0;
                    int numBytesToRead = (int)stream.Length;
                    stream.Position = 0;
                    while (numBytesToRead > 0)
                    {
                        int n = stream.Read(bytes, numBytesRead, Math.Min(numBytesToRead, int.MaxValue));
                        if (n <= 0)
                        {
                            break;
                        }
                        fs.Write(bytes, numBytesRead, n);
                        numBytesRead += n;
                        numBytesToRead -= n;
                    }
                    fs.Close();
                }
                




                return "<iframe src='/uploadimg#ok|/UploadImg/" + newname + "'></iframe>";
            };

            Get["uploadimg"] = p =>
            {
                return View["uploadimg"];
            };
        }
    }
}