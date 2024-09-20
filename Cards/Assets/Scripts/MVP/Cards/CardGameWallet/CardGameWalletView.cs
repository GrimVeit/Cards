using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardGameWalletView : View
{
    [SerializeField] private TextMeshProUGUI textMoney;

    public void SendMoneyDisplay(int coins)
    {
        textMoney.text = coins.ToString();
    }
}
