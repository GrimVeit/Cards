using System;

public class CardBetModel
{
    public event Action OnSubmitBet;
    public event Action OnActivate;
    public event Action OnDeactivate;

    public event Action<int> OnChangedBet;

    private BetAmounts betAmounts;

    private int bet;
    private int currentBetIndex = 0;

    public CardBetModel(BetAmounts betAmounts)
    {
        this.betAmounts = betAmounts;
    }

    public void Initialize()
    {
        currentBetIndex = 0;
        bet = betAmounts.betValues[currentBetIndex];
        OnChangedBet?.Invoke(bet);
    }

    public void Dispose()
    {

    }

    public void Activate()
    {
        OnActivate?.Invoke();

        SubmitBet();
    }

    public void Deactivate()
    {
        OnDeactivate?.Invoke();
    }

    public void IncreaseBet()
    {
        if (currentBetIndex < betAmounts.betValues.Count - 1)
        {
            currentBetIndex += 1;

            bet = betAmounts.betValues[currentBetIndex];
            OnChangedBet?.Invoke(bet);
        }
    }

    public void DecreaseBet()
    {
        if (currentBetIndex > 0)
        {
            currentBetIndex -= 1;

            bet = betAmounts.betValues[currentBetIndex];
            OnChangedBet?.Invoke(bet);
        }
    }

    public void SubmitBet()
    {
        if(IsBetActivated())
          OnSubmitBet?.Invoke();
    }

    public bool IsBetActivated()
    {
        return bet != 0 ? true : false;
    }
}
