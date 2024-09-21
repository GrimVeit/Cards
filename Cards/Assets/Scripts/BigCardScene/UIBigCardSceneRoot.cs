using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBigCardSceneRoot : MonoBehaviour
{
    [SerializeField] private MainPanel_BigCardScene mainPanel;
    [SerializeField] private MoveWinningsPanel_BigCardScene moveWinningsPanel;
    [SerializeField] private MoveMoneyPanel_BigCardScene moveMoneyPanel;

    private Panel currentPanel;

    public void Initialize()
    {
        mainPanel.Initialize();
        moveWinningsPanel.Initialize();
        moveMoneyPanel.Initialize();
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        moveWinningsPanel.Dispose();
        moveMoneyPanel.Dispose();
    }

    public void Activate()
    {
        OpenPanel(mainPanel);
    }

    public void Deactivate()
    {
        ClosePanel();
    }

    public void OpenMoveWinningsPanel()
    {
        OpenOtherPanel(moveWinningsPanel);
    }

    public void CloseMoveWinningsPanel()
    {
        CloseOtherPanel(moveWinningsPanel);
    }

    public void OpenMoveMoneyPanel()
    {
        OpenOtherPanel(moveMoneyPanel);
    }

    public void CloseMoveMoneyPanel()
    {
        CloseOtherPanel(moveMoneyPanel);
    }

    public void OpenPanel(Panel panel)
    {
        currentPanel?.DeactivatePanel();

        currentPanel = panel;
        currentPanel.ActivatePanel();
    }

    public void ClosePanel()
    {
        currentPanel?.DeactivatePanel();
    }

    public void OpenOtherPanel(Panel panel)
    {
        panel.ActivatePanel();
    }

    public void CloseOtherPanel(Panel panel)
    {
        panel.DeactivatePanel();
    }

    #region Input

    public event Action OnClickToBackButton
    {
        add { mainPanel.OnClickToBackButton += value; }
        remove { mainPanel.OnClickToBackButton -= value; }
    }

    public event Action OnClickToMoveWinningsButton
    {
        add { mainPanel.OnClickToMoveWinningsButton += value; }
        remove { mainPanel.OnClickToMoveWinningsButton -= value; }
    }

    public event Action OnClickToBackFromMoveWinningsButton
    {
        add { moveWinningsPanel.OnClickToBackButton += value; }
        remove { moveWinningsPanel.OnClickToBackButton -= value; }
    }

    public event Action OnClickToContinueGameButton
    {
        add { moveMoneyPanel.OnClickToContinueButton += value; }
        remove { moveMoneyPanel.OnClickToContinueButton -= value; }
    }

    #endregion
}
