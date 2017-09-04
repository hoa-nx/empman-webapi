using EmpMan.Web.Infrastructure.Core;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EmpMan.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Web API configuration and services
            config.EnableCors();
            // Web API routes
            config.MapHttpAttributeRoutes();

            /* JFormatter
            /** http://blog.developers.ba/replace-json-net-jil-json-serializer-asp-net-web-api/ **/

            //config.Formatters.RemoveAt(0);
            //config.Formatters.Insert(0, new JilFormatter());
            //http://krzysztofjakielaszek.com/2017/02/25/webapi-and-jil-json-serializer/
            /*
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
            new DefaultContractResolver { IgnoreSerializableAttribute = true };
            */

            config.ConfigureJSONFormatter();

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }

            );

        }

        private static void ConfigureJSONFormatter(this HttpConfiguration config)
        {
            // remove Json.NET
            // config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            // {
            //     ContractResolver = new CamelCasePropertyNamesContractResolver(),
            //     NullValueHandling = NullValueHandling.Ignore,
            //     DateFormatHandling = DateFormatHandling.IsoDateFormat
            // };

            // use Jil
            //config.Formatters.RemoveAt(0);
            //config.Formatters.Insert(0, new JilMediaTypeFormatter());

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
            new DefaultContractResolver { IgnoreSerializableAttribute = true };
        }
    }
}