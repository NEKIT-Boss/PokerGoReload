using System;
using System.Collections.Generic;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.Reflection;
using Windows.UI.Xaml;
using Prism.Events;
using Prism.Logging;
using Prism.MEF2.Services;
using Prism.Windows;
using Prism.Windows.AppModel;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using Prism.MEF2.Interfaces;

namespace Prism.MEF2
{
    public abstract class PrismMef2Application: PrismApplication, IDisposable
    {
        /// <summary>
        /// Ambient access to the Application
        /// </summary>
        public new static PrismMef2Application Current => (PrismMef2Application) Application.Current;

        /// <summary>
        /// Configured MEF <see cref="CompositionHost"/>.
        /// </summary>
        public CompositionHost Container { get; set; }

        /// <summary>
        /// The Conventions used by <see cref="ContainerConfiguration"/>.
        /// </summary>
        protected ConventionBuilder ConfigurationConventions { get; set; }

        /// <summary>
        /// Configuration intended to be used to create <see cref="Container"/>
        /// </summary>
        protected ContainerConfiguration ContainerConfiguration { get; set; }

        /// <summary>
        /// To make client's code more clear, when navigating for the first time.
        /// </summary>
        protected new IExtendedNavigationService NavigationService { get; private set; }

        /// <summary>
        /// All-in-one resolver used by Prism, to get VMs and stuff.
        /// </summary>
        /// <param name="type"><see cref="Type"/> to resolve the concrete <see cref="Type"/> for.</param>
        /// <returns>Resolved concrete <see cref="Type"/>.</returns>
        protected sealed override object Resolve(Type type)
        {
            return Container.GetExport(type);
        }

        /// <summary>
        /// Method override to support navigating by <see cref="Type"/> only.
        /// </summary>
        /// <param name="pageFullName">FullName of the page <see cref="Type"/> to navigate to.</param>
        /// <returns>Resolved concrete page <see cref="Type"/>.</returns>
        protected override Type GetPageType(string pageFullName)
        {
            var pageType = Type.GetType(pageFullName, false);
            if (pageType == null) throw new ArgumentException($"No such page type {pageFullName} exists!");

            return pageType;
        }

        #region ContainerConfigurations

        /// <summary>
        /// Performs default exports
        /// </summary>
        private void AddDefaultAssemblies()
        {
            const string prismAssemblyTitle = "Prism";
            const string prismWindowsAssemblyTitle = "Prism.Windows";

            var assemblies = new List<Assembly>
            {
                // Loading current assembly to, to get ExtendedNavigationServiceReferenced too.
                typeof(PrismMef2Application).GetTypeInfo().Assembly,
                Assembly.Load(new AssemblyName(prismAssemblyTitle)),
                Assembly.Load(new AssemblyName(prismWindowsAssemblyTitle))
            };

            ContainerConfiguration.WithAssemblies(assemblies);
        }

        /// <summary>
        /// Override this method to add assemblies to MEF configuration.
        /// </summary>
        protected virtual void AddAssemblies() { }

        /// <summary>
        /// Configures default export conventions.
        /// </summary>
        protected virtual void ConfigureConventions()
        {
            ConfigurationConventions = new ConventionBuilder();

            

            Logger.Log("Registering Prism services with container", Category.Debug, Priority.Low);
            ConfigurationConventions.ExportAllInterfaces<SessionStateService>(true);
            ConfigurationConventions.ExportAllInterfaces<DeviceGestureService>(true);
            ConfigurationConventions.ExportAllInterfaces<ExtendedNavigationService>(true);
            ConfigurationConventions.ExportAllInterfaces<EventAggregator>(true);

            // Wher using convention based export, MEF 2 is selecting constructor with the longest list of parameters.
            // So no ImportingConstructor decoration is required. If we use plain attribute based approach, then the ImportingConstructor
            // attribute is required.
            ConfigurationConventions.ForTypesDerivedFrom<ViewModelBase>().Export();
            ConfigurationConventions.ForTypesDerivedFrom<IModule>().Export<IModule>().Shared();
            
        }

        /// <summary>
        /// Configures conventions, loads default and specific assemblies, and creates <see cref="Container"/> with <see cref="ContainerConfiguration"/>.
        /// </summary>
        protected sealed override void CreateAndConfigureContainer()
        {
            ConfigureConventions();

            ContainerConfiguration = new ContainerConfiguration();
            ContainerConfiguration.WithDefaultConventions(ConfigurationConventions);

            AddDefaultAssemblies();
            AddAssemblies();

            Container = ContainerConfiguration.CreateContainer();
        }

        /// <summary>
        /// Creating the navigation service and registering it as a singletone, with provided rootFrame, with the composition host.
        /// </summary>
        /// <param name="rootFrame">Frame to register navigation service with.</param>
        /// <returns>Created and configured <see cref="INavigationService"/>.</returns>
        protected override INavigationService OnCreateNavigationService(IFrameFacade rootFrame)
        {
            var service = Container.GetExport<IExtendedNavigationService>();

            // Setting even registered services as the parameters, to provide consistency.
            service.Initialize(rootFrame, GetPageType, SessionStateService);

            // Because of the new statement in the declaration
            NavigationService = service;
            return service;
        }

        /// <summary>
        /// Creates the SessionStateService as a singleton through the composition host
        /// </summary>
        /// <returns>The SessionStateService</returns>
        protected override ISessionStateService OnCreateSessionStateService()
        {
            return Container.GetExport<ISessionStateService>();
        }

        /// <summary>
        /// Creates the DeviceGestureService as a singleton through the composition host
        /// </summary>
        /// <returns>DeviceGestureService instance</returns>
        protected override IDeviceGestureService OnCreateDeviceGestureService()
        {
            var svc = Container.GetExport<IDeviceGestureService>();
            svc.UseTitleBarBackButton = true;
            return svc;
        }

        /// <summary>
        /// Creates the IEventAggregator as a singleton through the container
        /// </summary>
        /// <returns>IEventAggregator instance</returns>
        protected override IEventAggregator OnCreateEventAggregator()
        {
            return Container.GetExport<IEventAggregator>();
        }

        #endregion

        #region IDisposable implementation

        public void Dispose()
        {
            Container?.Dispose();
            Container = null;
            GC.SuppressFinalize(this);
        }

        #endregion

        protected static Assembly GetAssembly<T>() where T: class
        {
            return typeof(T).GetTypeInfo().Assembly;
        }
    }
}
