using System;
using UnityEngine;

public class CardGameWalletPresenter : MonoBehaviour
{
    private CardGameWalletModel cardGameWalletModel;
    private CardGameWalletView cardGameWalletView;

    public CardGameWalletPresenter(CardGameWalletModel cardGameWalletModel, CardGameWalletView cardGameWalletView)
    {
        this.cardGameWalletModel = cardGameWalletModel;
        this.cardGameWalletView = cardGameWalletView;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        cardGameWalletModel.OnChangeMoney += cardGameWalletView.SendMoneyDisplay;
    }

    private void DeactivateEvents()
    {
        cardGameWalletModel.OnChangeMoney -= cardGameWalletView.SendMoneyDisplay;
    }

    #region Input

    public void SetBet(int bet)
    {
        cardGameWalletModel.SetBet(bet);
    }

    public void IncreaseMoney()
    {
        cardGameWalletModel.IncreseMoney();
    }

    public void DecreaseMoney()
    {
        cardGameWalletModel.DecreaseMoney();
    }

    public void TransitMoneyToBank()
    {
        cardGameWalletModel.TransitMoneyToBank();
    }

    public event Action OnMoneySuccesTransitToBank
    {
        add { cardGameWalletModel.OnMoneySuccesTransitToBank += value; }
        remove { cardGameWalletModel.OnMoneySuccesTransitToBank -= value; }
    }

    #endregion
}
