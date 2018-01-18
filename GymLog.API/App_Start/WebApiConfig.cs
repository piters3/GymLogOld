using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GymLog.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Filters.Add(new HostAuthenticationFilter(DefaultAuthenticationTypes.ApplicationCookie));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Add support CORS
            var attr = new EnableCorsAttribute("*", "*", "*") { SupportsCredentials = true };
            config.EnableCors(attr);

            //set json format
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // configure caching
            config.MessageHandlers.Add(new CacheCow.Server.CachingHandler(config));
        }
    }
}
