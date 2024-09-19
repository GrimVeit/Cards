using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardBetView : View
{
    public event Action OnIncreaseBet;
    public event Action OnDeacreaseBet;
    public event Action OnContinue;


    [SerializeField] private GameObject displayBet;
    [SerializeField] private Button increaseBetButton;
    [SerializeField] private Button decreaseBetButton;
    [SerializeField] private TextMeshProUGUI textBet;
    [SerializeField] private Button continueButton;


    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public void Activate()
    {
        increaseBetButton.onClick.AddListener(HandlerClickToIncreaseButton);
        decreaseBetButton.onClick.AddListener(HandlerClickToDecreaseButton);
        continueButton.onClick.AddListener(HandlerClickToContinue);

        displayBet.SetActive(true);
    }

    public void Deactivate()
    {
        increaseBetButton.onClick.RemoveListener(HandlerClickToIncreaseButton);
        decreaseBetButton.onClick.RemoveListener(HandlerClickToDecreaseButton);
        continueButton.onClick.RemoveListener(HandlerClickToContinue);

        displayBet.SetActive(false);
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

    private void HandlerClickToContinue()
    {
        OnContinue?.Invoke();
    }

    #endregion
}
