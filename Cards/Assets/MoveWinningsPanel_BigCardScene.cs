using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveWinningsPanel_BigCardScene : MovePanel
{
    [SerializeField] private Button buttonBack;

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        buttonBack.onClick.AddListener(HandlerClickToBackButton);
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        buttonBack.onClick.RemoveListener(HandlerClickToBackButton);
    }

    public event Action OnClickToBackButton;

    private void HandlerClickToBackButton()
    {
        OnClickToBackButton?.Invoke();
    }
}
