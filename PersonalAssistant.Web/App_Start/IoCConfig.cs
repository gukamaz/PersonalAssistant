using Autofac;
using Autofac.Integration.Mvc;
using PersonalAssistant.BLL.Utils;
using PersonalAssistant.DAL.Context;
using PersonalAssistant.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace PersonalAssistant.Web.App_Start
{
    public class DataModule : Autofac.Module
    {
        public DataModule()
        {
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PersonalAssistantContext>().As<IPersonalAssistantContext>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<Encryptor>().As<IEncryptor>().InstancePerRequest();
            builder.RegisterType<GeorgianToEnglishConverter>().As<ILangugageConverter>().InstancePerRequest();

            base.Load(builder);
        }
    }

    class IoCConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterSource(new ViewRegistrationSource());
            builder.RegisterFilterProvider();
            builder.RegisterModule(new DataModule());
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}