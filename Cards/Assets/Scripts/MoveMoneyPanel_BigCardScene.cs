using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveMoneyPanel_BigCardScene : MovePanel
{
    [SerializeField] private Button buttonContinue;

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        buttonContinue.onClick.AddListener(HandlerClickToContinueButton);
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        buttonContinue.onClick.RemoveListener(HandlerClickToContinueButton);
    }

    public event Action OnClickToContinueButton;

    private void HandlerClickToContinueButton()
    {
        OnClickToContinueButton?.Invoke();
    }
}
