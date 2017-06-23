using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using Poker.Core.Flow;
using Poker.Core.Game;
using Poker.Core.Players;
using PokerGo.Services;
using Prism.Windows.AppModel;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace PokerGo.ViewModels
{
    public class GameFlowPageViewModel : ViewModelBase
    {
        private Game _game;
        public Game Game
        {
            get => _game;
            private set => SetProperty(ref _game, value);
        }

        private Hand _currentHand;
        public Hand CurrentHand
        {
            get => _currentHand;
            private set => SetProperty(ref _currentHand, value);
        }

        private BettingRound _currentBettingRound;
        public BettingRound CurrentBettingRound
        {
            get => _currentBettingRound;
            private set => SetProperty(ref _currentBettingRound, value);
        }

        private Turn _currentTurn;
        public Turn CurrentTurn
        {
            get => _currentTurn;
            private set => SetProperty(ref _currentTurn, value);
        }

        #region TurnAmounts

        private int _betAmount;

        public int BetAmount
        {
            get => _betAmount;
            set => SetProperty(ref _betAmount, value);
        }

        private int _raiseAmount;

        public int RaiseAmount
        {
            get => _raiseAmount;
            set => SetProperty(ref _raiseAmount, value);
        }

        #endregion

        public GameFlowPageViewModel(IGameContextManager gameContextManager)
        {
            gameContextManager.CurrentGame = Game =
                new Game(gameContextManager.Players, gameContextManager.Configuration);

            StartGame();
        }

        private void StartGame()
        {
            CurrentHand = Game.NextHand();
            CurrentBettingRound = CurrentHand.NextBettingRound();
            CurrentTurn = CurrentBettingRound.NextTurn();

            BetAmount = Game.Configuration.MinimalRaise;
            RaiseAmount = Game.Configuration.MinimalRaise;
        }

        #region GameActions

        public void Check()
        {
            CurrentTurn.Check();
            AdvanceFlow();
        }

        public void Fold()
        {
            CurrentTurn.Fold();
            AdvanceFlow();
        }

        public void Bet()
        {
            CurrentTurn.Bet(BetAmount);
            AdvanceFlow();

            BetAmount = Game.Configuration.MinimalRaise;
        }

        public void Raise()
        {
            CurrentTurn.RaiseBy(RaiseAmount);
            AdvanceFlow();

            RaiseAmount = Game.Configuration.MinimalRaise;
        }

        public void Call()
        {
            CurrentTurn.Call();
            AdvanceFlow();
        }

        #endregion

        #region Advancing the Game

        private bool AdvanceTurn()
        {
            var nextTurn = CurrentBettingRound.NextTurn();

            if (nextTurn == null) return false;

            CurrentTurn = nextTurn;
            return true;
        }

        private bool AdvanceBettingRound()
        {
            var nextRound = CurrentHand.NextBettingRound();

            if (nextRound == null) return false;

            CurrentBettingRound = nextRound;
            return true;
        }

        private bool AdvanceHand()
        {
            var nextHand = Game.NextHand();

            if (nextHand == null) return false;

            CurrentHand = nextHand;
            return true;
        }

        private void AdvanceFlow()
        {
            if (!AdvanceTurn())
            {
                if (!AdvanceBettingRound())
                {
                    if (!AdvanceHand())
                    {
                        // Win the game here
                    }
                    // Mark the winner
                    AdvanceBettingRound();
                }
                AdvanceTurn();
            }
        }

        #endregion 
    }
}