using Autofac;
using MediatR;
using MediatR.Pipeline;
using Notebook.Core;
using Notebook.Core.Interfaces;
using Notebook.Infrastructure.Data;
using Notebook.SharedKernel;
using Notebook.SharedKernel.Interfaces;
using System.Reflection;
using Module = Autofac.Module;

namespace Notebook.Infrastructure
{
    public class DefaultInfrastructureModule : Module
    {
        private readonly bool _isDevelopment = false;
        private readonly List<Assembly> _assemblies = new List<Assembly>();

        public DefaultInfrastructureModule(bool isDevelopment, Assembly? callingAssembly = null)
        {
            _isDevelopment = isDevelopment;
            var coreAssembly = typeof(DefaultCoreModule).Assembly;
            var infrastructureAssembly = Assembly.GetAssembly(typeof(StartupSetup));
            _assemblies.Add(coreAssembly);

            if (infrastructureAssembly != null)
            {
                _assemblies.Add(infrastructureAssembly);
            }

            if (callingAssembly != null)
            {
                _assemblies.Add(callingAssembly);
            }
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (_isDevelopment)
            {
                RegisterDevelopmentOnlyDependencies(builder);
            }
            else
            {
                RegisterProductionOnlyDependencies(builder);
            }
            RegisterCommonDependencies(builder);
        }

        private void RegisterCommonDependencies(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepository<>))
                .As(typeof(IReadRepository<>))
                .InstancePerLifetimeScope();

            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            // MediatR 14 expects licensing services in the container.
            var licenseAccessorType = Type.GetType("MediatR.Licensing.LicenseAccessor, MediatR");
            if (licenseAccessorType != null)
            {
                builder.RegisterType(licenseAccessorType).AsSelf().SingleInstance();
            }

            var licenseValidatorType = Type.GetType("MediatR.Licensing.LicenseValidator, MediatR");
            if (licenseValidatorType != null)
            {
                builder.RegisterType(licenseValidatorType).AsSelf().SingleInstance();
            }

            builder
              .RegisterType<DomainEventDispatcher>()
              .As<IDomainEventDispatcher>()
              .InstancePerLifetimeScope();

            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(IRequestExceptionHandler<,,>),
                typeof(IRequestExceptionAction<,>),
                typeof(INotificationHandler<>),
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                .RegisterAssemblyTypes(_assemblies.ToArray())
                .AsClosedTypesOf(mediatrOpenType)
                .AsImplementedInterfaces();
            }

            builder.RegisterType<EmailSender>().As<IEmailSender>()
                .InstancePerLifetimeScope();
        }

        private void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
        {
        }

        private void RegisterProductionOnlyDependencies(ContainerBuilder builder)
        {
        }

    }
}
