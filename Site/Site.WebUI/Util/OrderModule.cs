using Ninject.Modules;
using Site.BLL.Interfaces;
using Site.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.WebUI.Util
{
    public class OrderModule : NinjectModule
    {
        private string connectionString;
        public OrderModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IOrderService>().To<OrderService>().WithConstructorArgument(connectionString);
        }
        //public override void Load()
        //{
        //    Bind<IOrderService>().To<OrderService>().;
        //}
    }
}