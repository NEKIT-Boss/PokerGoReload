using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Poker.Core;
using Poker.Core.Players;
using Prism.Commands;
using Prism.MEF2.Services;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace PokerGo.ViewModels
{
    public class TeamSetupPageViewModel : ViewModelBase
    {
        private readonly Random _random = new Random();
        private readonly IExtendedNavigationService _navigationService;

        public ObservableCollection<Player> Players { get; }

        public ObservableCollection<NamedColor> AvailableColors { get; private set; }

        public DelegateCommand AddPlayerCommand { get; }
        public DelegateCommand<Player> RemovePlayerCommand { get; }

        public DelegateCommand BackToSettingsCommand { get; }
        public DelegateCommand StartGameCommand { get; }

        public TeamSetupPageViewModel(IExtendedNavigationService navigationService)
        {
            _navigationService = navigationService;

            Players = new ObservableCollection<Player>();

            RemovePlayerCommand = new DelegateCommand<Player>(RemovePlayer);
            AddPlayerCommand = new DelegateCommand(AddPlayer, CanAddPlayer);

            Players.CollectionChanged += delegate
            {
                AddPlayerCommand.RaiseCanExecuteChanged();
                StartGameCommand.RaiseCanExecuteChanged();
            };

            BackToSettingsCommand = new DelegateCommand(BackToSettings);
            StartGameCommand = new DelegateCommand(StartGame, CanStartGame);
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            AvailableColors = new ObservableCollection<NamedColor>(NamedColor.PredefinedColors);

            CreateNewPlayer();
            CreateNewPlayer();
        }

        private bool CanStartGame()
        {
            return Players.Count > 1;
        }

        private void StartGame()
        {
            //_navigationService.Navigate<GameFlowPage>();
        }

        private void BackToSettings()
        {
            _navigationService.GoBack();
        }

        private bool CanAddPlayer()
        {
            return Players.Count < 9;
        }

        private void AddPlayer()
        {
            CreateNewPlayer();
        }

        private void RemovePlayer(Player player)
        {
            Players.Remove(player);
            AvailableColors.Add(player.Color);
        }

        private void CreateNewPlayer()
        {
            Players.Add(new Player("Player", NextColor()));
        }

        private NamedColor NextColor()
        {
            var rolledColor = AvailableColors[_random.Next(0, AvailableColors.Count)];
            AvailableColors.Remove(rolledColor);

            return rolledColor;
        }
    }
}