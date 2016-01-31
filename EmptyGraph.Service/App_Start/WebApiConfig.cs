using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EmptyGraph.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();
            
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Routes.MapHttpRoute(
            //    name: "LoginApi",
            //    routeTemplate: "api/login/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
