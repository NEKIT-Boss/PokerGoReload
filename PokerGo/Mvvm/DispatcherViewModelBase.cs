using System.Threading.Tasks;
using Windows.UI.Core;
using Prism.Windows.Mvvm;
using System;

namespace PokerGo.Mvvm
{
    public class DispatcherViewModelBase : ViewModelBase
    {
        public CoreDispatcher Dispatcher { private get; set; }

        protected async Task Dispatch(DispatchedHandler actions)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, actions);
        }
    }
}