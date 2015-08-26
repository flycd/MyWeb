using Nancy;
using Nancy.ErrorHandling;
using Nancy.Extensions;
using Nancy.IO;
using Nancy.ViewEngines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace MyWeb.Modules
{
    public class StatusCodeHandler : DefaultViewRenderer, IStatusCodeHandler 
    {

        #region 
        public StatusCodeHandler(IViewFactory factory)
            : base(factory)
        {
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            switch (statusCode)
            {
                case HttpStatusCode.NotFound:
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.ServiceUnavailable:
                case HttpStatusCode.NotImplemented:
                    return true;
                default:
                    return false;

            }


        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            var viewname = "Error";
            switch (statusCode)
            {

                case HttpStatusCode.NotFound:
                    viewname = "NotFound";
                    break;
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.ServiceUnavailable:
                case HttpStatusCode.NotImplemented:
                    viewname = "Error";
                    break;

            }

            var response = RenderView(context, viewname);
            response.StatusCode = statusCode;
            context.Response = response;
        }
        #endregion

       

    }
}