using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Poker.Core.Players;
using PokerGo.ViewModels;


namespace PokerGo.Views
{
    public sealed partial class SetWinnerContentDialog : ContentDialog
    {
        public SetWinnerContentDialog(SetWinnerContentDialogViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = viewModel;
            this.InitializeComponent();
        }

        public SetWinnerContentDialogViewModel ViewModel { get; }

        private void WinnersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DoneButton.IsEnabled = WinnersListBox.SelectedItems.Any();
        }

        private void DoneButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ViewModel.SetWinners( WinnersListBox.SelectedItems.Cast<InGamePlayer>().ToArray() );
            Hide();
        }
    }
}
