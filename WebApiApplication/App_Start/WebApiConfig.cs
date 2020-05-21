using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApiApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Web API configuration and services

            //JSON veri dönmesi icin
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            //Microsoft.AspNet.WebApi.Cors
            var enableCorsAttribute = new EnableCorsAttribute("*",
                                           "Origin, Content-Type, Accept",
                                           "GET, PUT, POST, DELETE, OPTIONS");
            config.EnableCors(enableCorsAttribute);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
