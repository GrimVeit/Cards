using System;

public class CardBetModel
{
    public event Action OnSubmitBet;
    public event Action<int> OnSubmitBet_Value;
    public event Action OnActivate;
    public event Action OnDeactivate;

    public event Action<int> OnChangedBet;

    private BetAmounts betAmounts;

    private int bet;
    private int currentBetIndex = 0;

    private ITutorialProvider tutorialProvider;

    public CardBetModel(BetAmounts betAmounts, ITutorialProvider tutorialProvider)
    {
        this.betAmounts = betAmounts;
        this.tutorialProvider = tutorialProvider;
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

        if (tutorialProvider.IsActiveTutorial())
            tutorialProvider.ActivateTutorial("ChooseMoneyBet");
    }

    public void Deactivate()
    {
        OnDeactivate?.Invoke();

        if (tutorialProvider.IsActiveTutorial())
            tutorialProvider.DeactivateTutorial("ChooseMoneyBet");
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
        if (IsBetActivated())
        {
            OnSubmitBet_Value?.Invoke(bet);
            OnSubmitBet?.Invoke();
        }
    }

    public bool IsBetActivated()
    {
        return bet != 0;
    }
}
