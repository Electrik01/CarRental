using Ninject;
using Site.BLL.Interfaces;
using Site.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using Owin;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(Site.WebUI.App_Start.Startup))]
namespace Site.WebUI.App_Start
{
    public class Startup
    {
        IServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService("CarRental");
        }
    }
}