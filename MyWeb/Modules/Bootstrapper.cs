using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Session;
using Nancy.TinyIoc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWeb.Modules
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.StaticContentsConventions.Clear();
            nancyConventions.StaticContentsConventions.Add
            (StaticContentConventionBuilder.AddDirectory("css", "/css"));
            nancyConventions.StaticContentsConventions.Add
            (StaticContentConventionBuilder.AddDirectory("js", "/script"));
            nancyConventions.StaticContentsConventions.Add
            (StaticContentConventionBuilder.AddDirectory("images", "/images"));
            nancyConventions.StaticContentsConventions.Add
          (StaticContentConventionBuilder.AddDirectory("UploadImg", "/UploadImg"));           
            StaticConfiguration.DisableErrorTraces = true;
           
        }
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            CookieBasedSessions.Enable(pipelines);
        }

        protected override IRootPathProvider RootPathProvider
        {
            get
            {
                return new SiteRootPath();
            }
        }

         
    }
}