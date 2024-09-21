using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_BigCardScene : MovePanel
{
    //[SerializeField] private Button buttonBack;
    [SerializeField] private Button buttonMoveWinnings;

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        //buttonBack.onClick.AddListener(HandlerClickToBackButton);
        buttonMoveWinnings.onClick.AddListener(HandlerClickToMoveWinningsButton);
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        //buttonBack.onClick.RemoveListener(HandlerClickToBackButton);
        buttonMoveWinnings.onClick.RemoveListener(HandlerClickToMoveWinningsButton);
    }

    #region Input

    public event Action OnClickToBackButton;
    public event Action OnClickToMoveWinningsButton;

    private void HandlerClickToBackButton()
    {
        OnClickToBackButton?.Invoke();
    }

    private void HandlerClickToMoveWinningsButton()
    {
        OnClickToMoveWinningsButton?.Invoke();
    }

    #endregion
}
