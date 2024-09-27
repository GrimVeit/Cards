using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LosePanel_BigCardScene : MovePanel
{
    public event Action OnClickToExitButton;
    public event Action OnClickToContinueButton;

    [SerializeField] private Button continueButton;
    [SerializeField] private Button exitButton;

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        continueButton.onClick.AddListener(HandlerClickToContinueButton);
        exitButton.onClick.AddListener(HandlerClickToExitButton);
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        continueButton.onClick.RemoveListener(HandlerClickToContinueButton);
        exitButton.onClick.RemoveListener(HandlerClickToExitButton);
    }

    private void HandlerClickToExitButton()
    {
        OnClickToExitButton?.Invoke();
    }

    private void HandlerClickToContinueButton()
    {
        OnClickToContinueButton?.Invoke();
    }
}
