using Autofac;
using HomeBanking.API.Application.Queries;
using HomeBanking.Domain.AggregatesModel.CategoryAggregate;
using HomeBanking.Domain.AggregatesModel.FamilyAggregate;
using HomeBanking.Domain.AggregatesModel.UserAggregate;
using HomeBanking.Domain.AggregatesModel.WalletAggregate;
using HomeBanking.Infrastructure.Idempotency;
using HomeBanking.Infrastructure.Repositories;

namespace HomeBanking.API.Infrastructure.AutofacModules
{
    public class ApplicationModule
        : Autofac.Module
    {

        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ReportQueries(QueriesConnectionString))
                .As<IReportQueries>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CategoryRepository>()
                .As<ICategoryRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<FamilyRepository>()
                .As<IFamilyRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserRepository>()
               .As<IUserRepository>()
               .InstancePerLifetimeScope();

            builder.RegisterType<WalletRepository>()
               .As<IWalletRepository>()
               .InstancePerLifetimeScope();

            builder.RegisterType<RequestManager>()
               .As<IRequestManager>()
               .InstancePerLifetimeScope();

        }
    }
}
