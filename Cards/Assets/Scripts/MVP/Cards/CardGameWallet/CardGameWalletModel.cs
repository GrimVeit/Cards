using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameWalletModel
{
    public int Money { get; private set; }
    public event Action OnMoneySuccesTransitToBank;
    public event Action<int> OnChangeMoney;

    //private IMoneyProvider moneyProvider;

    private int bet;

    public CardGameWalletModel()
    {
        //this.moneyProvider = moneyProvider;
    }

    public void Initialize()
    {
        Money = 0;
        OnChangeMoney?.Invoke(Money);
    }

    public void Destroy()
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
            Money = bet;
            OnChangeMoney?.Invoke(Money);
            return;
        }

        Money *= bet;
        OnChangeMoney?.Invoke(Money);
    }

    public void DecreaseMoney()
    {
        Money = 0;
        OnChangeMoney?.Invoke(Money);
    }

    public void TransitMoneyToBank()
    {
        //moneyProvider.SendMoney(Money);
        Money = 0;
        OnChangeMoney?.Invoke(Money);

        OnMoneySuccesTransitToBank?.Invoke();
    }
}
