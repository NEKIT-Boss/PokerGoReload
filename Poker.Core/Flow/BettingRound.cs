using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Prism.Mvvm;

namespace Poker.Core.Flow
{
    public class BettingRound : BindableBase, IEnumerator<Turn>
    {
        /// <summary>
        /// Name of the round, connected with the kind of Poker we are playing.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Proxying out the pot of the current hand.
        /// </summary>
        public Pot Pot => CurrentHand.Pot;

        /// <summary>
        /// Proxying out the current bet of the hand.
        /// </summary>
        public int CurrentBet
        {
            get => CurrentHand.CurrentBet;
            set => CurrentHand.CurrentBet = value;
        }

        private bool _isOpened;

        public bool IsOpened
        {
            get => _isOpened;
            set => SetProperty(ref _isOpened, value);
        }

        public Turn NextTurn()
        {
            bool moved = _turnsEnumerator.MoveNext();
            return moved ? _turnsEnumerator.Current : null;
        }

        private IEnumerable<Turn> CyclePlayers()
        {
            return CurrentHand.Table
                .Select(player => new Turn(this, player));
        }

        private readonly IEnumerator<Turn> _turnsEnumerator;

        private IEnumerable<Turn> Turns
        {
            get
            {
                // First cycle
                foreach (var turn in CyclePlayers())
                {
                    yield return turn;
                }

                // Until all are agreed with the bet
                while (!CurrentHand.IsEquallyBet)
                {
                    foreach (var turn in CyclePlayers())
                    {
                        if (!CurrentHand.Table.IsPlaying) yield break;

                        yield return turn;
                    }
                }
            }
        }

        /// <summary>
        /// Part of chain of responsibility
        /// </summary>
        private Hand CurrentHand { get; }

        /// <summary>
        /// Just creating it only parameterized
        /// </summary>
        /// <param name="currentHand">Reference to the hand, this betting round is taking place in.</param>
        /// <param name="name">Name of the betting round.</param>
        public BettingRound(Hand currentHand, string name)
        {
            Name = name;
            CurrentHand = currentHand;
            IsOpened = false;

            _turnsEnumerator = Turns.GetEnumerator();
        }

        #region IEnumerator implementation

        public bool MoveNext()
        {
            var nextTurn = NextTurn();
            if (nextTurn == null) return false;

            Current = nextTurn;
            return true;
        }

        public void Reset()
        {
            throw new InvalidOperationException("Can not reset this enumerator!");
        }

        public Turn Current { get; private set; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            _turnsEnumerator.Dispose();
        }

        #endregion
    }
}