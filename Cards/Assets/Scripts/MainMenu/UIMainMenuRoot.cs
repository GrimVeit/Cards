using System;
using UnityEngine;

public class UIMainMenuRoot : MonoBehaviour
{
    [SerializeField] private MainPanel_MainMenuScene mainPanel;
    [SerializeField] private DailyBonusPanel_MainMenuScene dailyBonusPanel;
    [SerializeField] private DailyRewardPanel_MainMenuScene dailyRewardPanel;

    private bool isCooldownDailyRewardPanelActivated;
    private bool isCooldownDailyBonusPanelActivated;

    //private ISoundProvider soundProvider;
    //private IParticleEffectProvider particleEffectProvider;

    private Panel currentPanel;

    public void Initialize()
    {
        mainPanel.Initialize();
        dailyBonusPanel.Initialize();
        dailyRewardPanel.Initialize();
    }

    public void Activate()
    {
        mainPanel.GoToMiniGame_Action += HandlerGoToMiniGame;

        dailyRewardPanel.OnClickBackButton += OpenMainPanel;
        dailyBonusPanel.OnClickBackButton += OpenMainPanel;
    }

    public void Deactivate()
    {
        mainPanel.GoToMiniGame_Action -= HandlerGoToMiniGame;

        dailyRewardPanel.OnClickBackButton -= OpenMainPanel;
        dailyBonusPanel.OnClickBackButton -= OpenMainPanel;
    }

    public void SetSoundProvider(ISoundProvider soundProvider)
    {
        //this.soundProvider = soundProvider;
    }

    public void SetParticleEffectProvider(IParticleEffectProvider particleEffectProvider)
    {
        //this.particleEffectProvider = particleEffectProvider;
    }

    public void Dispose()
    {
        mainPanel.Dispose();
        dailyBonusPanel.Dispose();
        dailyRewardPanel.Dispose();
    }


    private void OpenPanel(Panel panel)
    {
        if (currentPanel != null)
            currentPanel.DeactivatePanel();

        //soundProvider.PlayOneShot("ShoohPanel_Open");
        currentPanel = panel;
        currentPanel.ActivatePanel();

    }

    private void OpenOtherPanel(Panel panel)
    {
        panel.ActivatePanel();
    }

    private void CloseOtherPanel(Panel panel)
    {
        panel.DeactivatePanel();
    }

    public void OpenMainPanel()
    {
        OpenPanel(mainPanel);
    }

    public void OpenDailyBonusPanel()
    {
        OpenPanel(dailyBonusPanel);
    }

    public void OpenDailyRewardPanel()
    {
        OpenPanel(dailyRewardPanel);
    }





    //public void OpenCooldownDailyRewardPanel()
    //{
    //    isCooldownDailyRewardPanelActivated = true;
    //    OpenOtherPanel(cooldownDailyRewardPanel);
    //}

    //public void CloseCooldownDailyRewardPanel()
    //{
    //    if (!isCooldownDailyRewardPanelActivated) return;

    //    isCooldownDailyRewardPanelActivated = false;
    //    CloseOtherPanel(cooldownDailyRewardPanel);
    //}

    //public void OpenCooldownDailyBonusPanel()
    //{
    //    OpenOtherPanel(cooldownDailyBonusPanel);
    //}

    //public void CloseCooldownDailyBonusPanel()
    //{
    //    CloseOtherPanel(cooldownDailyBonusPanel);
    //}


    private void HandlerGoToMiniGame()
    {
        currentPanel.DeactivatePanel();

        GoToMiniGame_Action?.Invoke();
    }

    #region Input Actions

    public event Action OnActivateMainMenuPanel
    {
        add { mainPanel.OnOpenPanel += value; }
        remove { mainPanel.OnOpenPanel -= value; }
    }
    public event Action OnDeactivateMainMenuPanel
    {
        add { mainPanel.OnClosePanel += value; }
        remove { mainPanel.OnClosePanel -= value; }
    }

    public event Action GoToMiniGame_Action;

    #endregion
}
