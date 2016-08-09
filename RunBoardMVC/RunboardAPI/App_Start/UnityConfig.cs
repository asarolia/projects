using Microsoft.Practices.Unity;
using RunboardAPI.DataAccessLayer;
using RunboardAPI.Models;
using System.Web.Http;
using Unity.WebApi;

namespace RunboardAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
           // container.RegisterType<IDataAccessLayer<batchdata_view>, clsDataAccessLayer>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}