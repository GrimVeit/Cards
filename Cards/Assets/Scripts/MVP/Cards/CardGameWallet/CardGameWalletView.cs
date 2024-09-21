using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardGameWalletView : View
{
    [SerializeField] private Button buttonTransferMoneyToBank;
    [SerializeField] private List<BankDisplayView> bankDisplayView = new List<BankDisplayView>();

    public void Initialize()
    {
        for (int i = 0; i < bankDisplayView.Count; i++)
        {
            bankDisplayView[i].Initialize();
        }

        buttonTransferMoneyToBank.onClick.AddListener(HandlerClickToTransferMoneyToBank);
    }

    public void Dispose()
    {
        for (int i = 0; i < bankDisplayView.Count; i++)
        {
            bankDisplayView[i].Initialize();
        }

        buttonTransferMoneyToBank.onClick.RemoveListener(HandlerClickToTransferMoneyToBank);
    }

    public void SendMoneyDisplay(int coins)
    {
        for (int i = 0; i < bankDisplayView.Count; i++)
        {
            bankDisplayView[i].SendMoneyDisplay(coins);
        }
    }

    #region Input

    public event Action OnClickToTransferMoneyToBankButton;

    private void HandlerClickToTransferMoneyToBank()
    {
        OnClickToTransferMoneyToBankButton?.Invoke();
    }

    #endregion
}
