using Microsoft.AspNet.SignalR;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using EmpMan.Web.Mappings;
using EmpMan.Web.Infrastructure.JobScheduler;

namespace EmpMan.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfiguration.Configure();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            /*
             * To deploy an application that uses spatial data types to a machine that 
             * does not have 'System CLR Types for SQL Server' installed you also need 
             * to deploy the native assembly SqlServerSpatial140.dll. Both x86 (32 bit) 
             * and x64 (64 bit) versions of this assembly have been added to your project 
             * under the SqlServerTypes\x86 and SqlServerTypes\x64 subdirectories. The 
             * native assembly msvcr120.dll is also included in case the C++ runtime 
             * is not installed.You need to add code to load the correct one of these 
             * assemblies at runtime (depending on the current architecture). 
             */
            //For ASP.NET Web Applications, add the following line of code to the Application_Start method in Global.asax.cs: 
            //SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));
            //start job de thuc thi cong viec dinh ky
            //JobScheduler.Start();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            Context.Response.AppendHeader("Access-Control-Allow-Credentials", "true");
            var referrer = Request.UrlReferrer;
            if (Context.Request.Path.Contains("signalr/") && referrer != null)
            {
                Context.Response.AppendHeader("Access-Control-Allow-Origin", referrer.Scheme + "://" + referrer.Authority);
            }
        }

        protected void Application_PostAuthorizeRequest()
        {
            //System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }

    }
}