using Autofac;
using Autofac.Integration.Mvc;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TravelAssistApp.Infrastructure;
using TravelAssistApp.Models;
using TravelAssistApp.Repository;
using TravelAssistApp.Service;

namespace TravelAssistApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();

            //Register dependencies in controller
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Services
            builder.RegisterAssemblyTypes(typeof(TouristPlacesService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces()
               .InstancePerRequest();

            // Repository
            builder.RegisterAssemblyTypes(typeof(TouristPlacesRepository).Assembly)
               .Where(t => t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces()
               .InstancePerRequest();

            builder.RegisterType(typeof(ApplicationDbContext)).As(typeof(DbContext)).InstancePerLifetimeScope();
            //UnitOfWork
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerHttpRequest();

            //Register dependencies in filter attributes
            builder.RegisterFilterProvider();

            //Register dependencies in Custom Views
            builder.RegisterSource(new ViewRegistrationSource());

            //Register our Data dependencies
            builder.RegisterModule(new DataModule("TravelAssistContext"));

            var container = builder.Build();

            //Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
