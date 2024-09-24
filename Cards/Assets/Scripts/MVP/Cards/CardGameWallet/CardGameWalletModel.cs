using System;

public class CardGameWalletModel
{
    public int Money { get; private set; }
    public event Action OnMoneySuccesTransitToBank;
    public event Action<int> OnChangeMoney;
    public event Action<int> OnAddMoney;
    public event Action<int> OnRemoveMoney;

    private IMoneyProvider moneyProvider;

    private int bet;
    private int multiply;

    public CardGameWalletModel(IMoneyProvider moneyProvider)
    {
        this.moneyProvider = moneyProvider;
    }

    public void Initialize()
    {
        Money = 0;
        OnChangeMoney?.Invoke(Money);
    }

    public void Dispose()
    {

    }

    public void SetBet(int bet)
    {
        this.bet = bet;
    }

    public void IncreseMoney()
    {
        if(Money == 0)
        {
            multiply = bet;
            Money = multiply;
            OnAddMoney?.Invoke(multiply);
            OnChangeMoney?.Invoke(Money);
            return;
        }

        multiply *= multiply;
        OnAddMoney?.Invoke(multiply);

        Money += multiply;
        OnChangeMoney?.Invoke(Money);
    }

    public void DecreaseMoney()
    {
        OnRemoveMoney?.Invoke(Money);

        multiply = 0;
        Money = multiply;
        OnChangeMoney?.Invoke(Money);
    }

    public void TransitMoneyToBank()
    {
        moneyProvider.SendMoney(Money);
        multiply = 0;
        Money = multiply;
        OnChangeMoney?.Invoke(Money);

        OnMoneySuccesTransitToBank?.Invoke();
    }
}
