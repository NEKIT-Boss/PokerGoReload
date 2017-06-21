using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using CommandingProxy;
using Poker.Core;
using PokerGo.Views;
using Prism.MEF2;
using Prism.MEF2.Interfaces;

namespace PokerGo
{
    sealed partial class App : PrismMef2Application
    {
        private IEnumerable<IModule> _modules;

        public App()
        {
            InitializeComponent();
            UnhandledException += OnUnhandledException;
        }

        private void Initialize()
        {
            _modules = Container.GetExports<IModule>();
        }

        protected override void AddAssemblies()
        {
            ContainerConfiguration
                .WithAssembly(GetAssembly<App>())
                .WithAssembly(GetAssembly<PokerCoreModule>())
                .WithAssembly(GetAssembly<CommandingProxyModule>());
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            Initialize();

            NavigationService.Navigate<HomePage>();
            return Task.CompletedTask;
        }

        private static void OnUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            Debug.WriteLine(e.Message);
            Debugger.Break();
        }
    }
}
