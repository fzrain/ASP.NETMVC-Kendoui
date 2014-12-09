using Autofac;
using Autofac.Integration.Mvc;
using Fzrain.Core.Data;
using Fzrain.Core.Infrastructure;
using Fzrain.Data;
using Fzrain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fzrain.Core.Infrastructure.DependencyManagement;

namespace Fzrain.Web.Framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());
            //数据访问层相关依赖
            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();
            //todo:暂时把连接字符串写死,测试时使用
            dataProviderSettings.DataConnectionString = "Data Source=.;Initial Catalog=FzrainFramework;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=11111";
            dataProviderSettings.DataProvider = "sqlserver";
            builder.Register(c => dataProviderSettings).As<DataSettings>();
            builder.Register(x => new EfDataProviderManager(x.Resolve<DataSettings>())).As<BaseDataProviderManager>().InstancePerDependency();


            builder.Register(x => x.Resolve<BaseDataProviderManager>().LoadDataProvider()).As<IDataProvider>().InstancePerDependency();

            if (dataProviderSettings != null && dataProviderSettings.IsValid())
            {
                var efDataProviderManager = new EfDataProviderManager(dataProviderSettings);
                var dataProvider = efDataProviderManager.LoadDataProvider();
                dataProvider.InitConnectionFactory();

                builder.Register<IDbContext>(c => new FzrainContext(dataProviderSettings.DataConnectionString)).InstancePerLifetimeScope();
            }
            else
            {
                builder.Register<IDbContext>(c => new FzrainContext(dataSettingsManager.LoadSettings().DataConnectionString)).InstancePerLifetimeScope();
            }

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

       
            //service
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        }

        public int Order { get { return 0; } }
    }
}
