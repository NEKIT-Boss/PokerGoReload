using System.Collections.Generic;
using Infrastructure.Commanding.HubEvents;
using Infrastructure.Commanding.HubProxies;
using PokerGo.Mvvm;
using Prism.Windows.Navigation;

namespace PokerGo.ViewModels
{
    public class HomePageViewModel : DispatcherViewModelBase, ISamplingEvents
    {
        private readonly ICommandingHub<ISamplingEvents> _samplingHubProxy;

        private int _successCounter;

        public int SuccessCounter
        {
            get => _successCounter;
            set => SetProperty(ref _successCounter, value);
        }

        private int _failCounter;

        public int FailCounter
        {
            get => _failCounter;
            set => SetProperty(ref _failCounter, value);
        }

        public HomePageViewModel(ICommandingHub<ISamplingEvents> samplingHubProxy)
        {
            _samplingHubProxy = samplingHubProxy;
        }

        async void ISamplingEvents.RecognitionSucceeded()
        {
            await Dispatch(() => SuccessCounter++);
        }

        async void ISamplingEvents.RecognitionFailed(string errorMessage)
        {
            await Dispatch(() => FailCounter++);
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            _samplingHubProxy.AddSubscriber(this);
            await _samplingHubProxy.Connect();
        }
    }
}