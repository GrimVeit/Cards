using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardWalletView : MonoBehaviour
{
    public event Action OnIncreaseBet;
    public event Action OnDeacreaseBet;


    [SerializeField] private GameObject displayBet;
    [SerializeField] private Button increaseBetButton;
    [SerializeField] private Button decreaseBetButton;
    [SerializeField] private TextMeshProUGUI textBet;


    public void Initialize()
    {
        increaseBetButton.onClick.AddListener(HandlerClickToIncreaseButton);
        decreaseBetButton.onClick.AddListener(HandlerClickToDecreaseButton);
    }

    public void Dispose()
    {
        increaseBetButton.onClick.RemoveListener(HandlerClickToIncreaseButton);
        decreaseBetButton.onClick.RemoveListener(HandlerClickToDecreaseButton);
    }

    public void DisplayBet(int bet)
    {
        textBet.text = bet.ToString();
    }

    #region Input

    private void HandlerClickToIncreaseButton()
    {
        OnIncreaseBet?.Invoke();
    }

    private void HandlerClickToDecreaseButton()
    {
        OnDeacreaseBet?.Invoke();
    }

    #endregion
}
