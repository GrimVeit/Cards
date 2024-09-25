using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_MainMenuScene : MovePanel
{
    public event Action OnOpenPanel;
    public event Action OnClosePanel;

    [SerializeField] private Button miniGame_Button;

    public event Action GoToMiniGame_Action;

    public override void ActivatePanel()
    {
        OnOpenPanel?.Invoke();

        base.ActivatePanel();

        miniGame_Button.onClick.AddListener(HandleGoToMiniGame_ButtonClick);
    }

    public override void DeactivatePanel()
    {
        OnClosePanel?.Invoke();

        base.DeactivatePanel();

        miniGame_Button.onClick.RemoveListener(HandleGoToMiniGame_ButtonClick);
    }

    private void HandleGoToMiniGame_ButtonClick()
    {
        GoToMiniGame_Action?.Invoke();
    }
}
