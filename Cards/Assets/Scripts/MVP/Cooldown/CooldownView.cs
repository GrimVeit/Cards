using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CooldownView : View, IIdentify
{
    [SerializeField] private string viewID;

    public event Action OnClickCooldownButton;

    [SerializeField] private Button cooldownButton;
    [SerializeField] private TextMeshProUGUI textCountdown;

    public string GetID() => viewID;

    public void Initialize()
    {
        cooldownButton.onClick.AddListener(HandlerClickToCooldownButton);
    }

    public void Dispose()
    {
        cooldownButton.onClick.RemoveListener(HandlerClickToCooldownButton);
    }

    public void ChangeTimer(string time)
    {
        textCountdown.text = time;
    }

    public void OnActivateButton()
    {

    }

    public void OnDeactivateButton()
    {

    }

    private void HandlerClickToCooldownButton()
    {
        OnClickCooldownButton?.Invoke();
    }
}
