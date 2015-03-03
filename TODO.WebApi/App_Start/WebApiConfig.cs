using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using TODO.WebApi.Filters;

namespace TODO.WebApi
{
    public static class WebApiConfig
    {
        public static HttpConfiguration Register()
        {

            var config = new HttpConfiguration();
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new ValidateModelAttribute());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            

            return config;
        }
    }
}
