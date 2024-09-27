using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBetPresenter
{
    private CardBetModel cardWalletModel;
    private CardBetView cardWalletView;

    public CardBetPresenter(CardBetModel cardWalletModel, CardBetView cardWalletView)
    {
        this.cardWalletModel = cardWalletModel;
        this.cardWalletView = cardWalletView;
    }

    public void Initialize()
    {
        ActivateEvents();

        cardWalletModel.Initialize();
        cardWalletView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        //cardWalletModel.Dispose();
        //cardWalletView.Dispose()
    }

    private void ActivateEvents()
    {
        cardWalletView.OnIncreaseBet += cardWalletModel.IncreaseBet;
        cardWalletView.OnDeacreaseBet += cardWalletModel.DecreaseBet;
        cardWalletView.OnContinue += cardWalletModel.SubmitBet;

        cardWalletModel.OnChangedBet += cardWalletView.DisplayBet;
        cardWalletModel.OnActivate += cardWalletView.Activate;
        cardWalletModel.OnDeactivate += cardWalletView.Deactivate;
    }

    private void DeactivateEvents()
    {
        cardWalletView.OnIncreaseBet -= cardWalletModel.IncreaseBet;
        cardWalletView.OnDeacreaseBet -= cardWalletModel.DecreaseBet;
        cardWalletView.OnContinue -= cardWalletModel.SubmitBet;

        cardWalletModel.OnChangedBet -= cardWalletView.DisplayBet;
        cardWalletModel.OnActivate -= cardWalletView.Activate;
        cardWalletModel.OnDeactivate -= cardWalletView.Deactivate;
    }

    #region Input

    public void Activate()
    {
        cardWalletModel.Activate();
    }

    public void Deactivate()
    {
        cardWalletModel.Deactivate();
    }

    public bool IsBetActivated()
    {
        return cardWalletModel.IsBetActivated();
    }

    public event Action OnSubmitBet
    {
        add { cardWalletModel.OnSubmitBet += value; }
        remove { cardWalletModel.OnSubmitBet -= value; }
    }

    public event Action<int> OnSubmitBet_Value
    {
        add { cardWalletModel.OnSubmitBet_Value += value; }
        remove { cardWalletModel.OnSubmitBet_Value -= value; }
    }

    #endregion
}
