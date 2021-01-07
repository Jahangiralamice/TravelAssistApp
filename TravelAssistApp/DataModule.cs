using Autofac;
using TravelAssistApp.Models;

namespace TravelAssistApp
{
    //The class inherits from Module and overrides the load method.
    //The load method takes an instance of ContainerBuilder

    public class DataModule : Module
    {
        private string _connStr;
        public DataModule(string connStr)
        {
            _connStr = connStr;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ApplicationDbContext(_connStr)).InstancePerRequest();
            base.Load(builder);
        }
    }
}