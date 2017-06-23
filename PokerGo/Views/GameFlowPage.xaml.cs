using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using PokerGo.ViewModels;


namespace PokerGo.Views
{
    public sealed partial class GameFlowPage : Page
    {
        public GameFlowPage()
        {
            this.InitializeComponent();

            BetButton.Click += HideBetFlyout;
            RaiseButton.Click += HideRaiseFlyout;
        }

        private GameFlowPageViewModel _viewModel;

        public GameFlowPageViewModel ViewModel =>
            _viewModel ?? (_viewModel = (GameFlowPageViewModel) DataContext);

        private void OpenAttachedFlyout(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }

        private void HideBetFlyout(object sender, RoutedEventArgs e)
        {
            BetFlyout.Hide();
        }

        private void HideRaiseFlyout(object sender, RoutedEventArgs routedEventArgs)
        {
            RaiseFlyout.Hide();
        }
    }
}
