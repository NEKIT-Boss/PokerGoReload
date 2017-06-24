using System;
using Poker.Core.Players;

namespace Poker.Core.Flow
{
    public class Turn
    {
        public InGamePlayer CurrentPlayer { get; }

        private BettingRound CurrentBettingRound { get; }

        public Turn(BettingRound currentBettingRound, InGamePlayer player)
        {
            CurrentPlayer = player;
            CurrentBettingRound = currentBettingRound;
        }

        #region GameActions

        public void Check()
        {
            if (CurrentBettingRound.IsOpened) throw new InvalidOperationException("Can not check in opened round!");
            // Actually here we are not doing anything.
            // Later we could add some decorated things.
        }

        public void Call()
        {
            if (!CurrentBettingRound.IsOpened) throw new InvalidOperationException("Can not call while round is not opened!");

            
            CurrentPlayer.Bet(CurrentBettingRound.CurrentBet - CurrentPlayer.CurrentBet);
        }

        public void Bet(int amount)
        {
            if (CurrentBettingRound.IsOpened) throw new InvalidOperationException();

            CurrentPlayer.Bet(amount);

            CurrentBettingRound.CurrentBet += amount;
            CurrentBettingRound.IsOpened = true;
        }

        public void RaiseBy(int amount)
        {
            if (!CurrentBettingRound.IsOpened) throw new InvalidOperationException();

            CurrentPlayer.Bet(CurrentBettingRound.CurrentBet - CurrentPlayer.CurrentBet + amount);

            CurrentBettingRound.CurrentBet += amount;
        }

        public void RaiseTo(int bet)
        {
            if (!CurrentBettingRound.IsOpened) throw new InvalidOperationException();
            if (CurrentBettingRound.CurrentBet >= bet) throw new InvalidOperationException();

            CurrentPlayer.Bet(bet - CurrentBettingRound.CurrentBet);

            CurrentBettingRound.CurrentBet = bet;
        }

        public void Fold()
        {
            CurrentPlayer.Fold();
        }

        #endregion
    }
}