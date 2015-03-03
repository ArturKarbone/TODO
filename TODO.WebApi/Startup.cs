using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using TODO.WebApi;

[assembly: OwinStartup(typeof(Startup))]
namespace TODO.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.UseNinjectMiddleware(NinjectWebCommon.CreateKernel).UseNinjectWebApi(WebApiConfig.Register());
        }
    }
}