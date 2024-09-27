using System;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UIMainMenuRoot menuRootPrefab;

    private UIMainMenuRoot sceneRoot;
    private ViewContainer viewContainer;

    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;
    private BankPresenter bankPresenter;

    private DailyRewardPresenter dailyRewardPresenter;
    private DailyBonusPresenter dailyBonusPresenter;

    private CooldownPresenter cooldownDailyRewardPresenter;
    private CooldownPresenter cooldownDailyBonusPresenter;


    private ISoundProvider soundProvider;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = Instantiate(menuRootPrefab);
 
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter
            (new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS),
            viewContainer.GetView<SoundView>());
        soundPresenter.Initialize();

        particleEffectPresenter = new ParticleEffectPresenter
            (new ParticleEffectModel(),
            viewContainer.GetView<ParticleEffectView>());
        particleEffectPresenter.Initialize();

        cooldownDailyRewardPresenter = new CooldownPresenter
            (new CooldownModel(PlayerPrefsKeys.NEXT_DAILY_REWARD_TIME, TimeSpan.FromDays(1)),
            viewContainer.GetView<CooldownView>("DailyReward"));
        cooldownDailyRewardPresenter.Initialize();
         
        cooldownDailyBonusPresenter = new CooldownPresenter
            (new CooldownModel(PlayerPrefsKeys.NEXT_DAILY_BONUS_TIME, TimeSpan.FromDays(1)),
            viewContainer.GetView<CooldownView>("DailyBonus"));
        cooldownDailyBonusPresenter.Initialize();

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        bankPresenter.Initialize();

        dailyRewardPresenter = new DailyRewardPresenter
            (new DailyRewardModel(soundPresenter, particleEffectPresenter),
            viewContainer.GetView<DailyRewardView>());
        dailyRewardPresenter.Initialize();

        dailyBonusPresenter = new DailyBonusPresenter(new DailyBonusModel(), viewContainer.GetView<DailyBonusView>());
        dailyBonusPresenter.Initialize();

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.SetParticleEffectProvider(particleEffectPresenter);
        sceneRoot.Initialize();


        ActivateTransitionsSceneEvents();
        ActivateEvents();

        sceneRoot.Activate();
        cooldownDailyRewardPresenter.Activate();
        cooldownDailyBonusPresenter.Activate();
    }

    private void ActivateTransitionsSceneEvents()
    {
        sceneRoot.GoToMiniGame_Action += HandleGoToMiniGame;
    }

    private void DeactivateTransitionsSceneEvents()
    {
        sceneRoot.GoToMiniGame_Action -= HandleGoToMiniGame;
    }

    private void ActivateEvents()
    {
        dailyRewardPresenter.OnGetDailyReward += cooldownDailyRewardPresenter.ActivateCooldown;
        dailyRewardPresenter.OnGetDailyReward += sceneRoot.OpenMainPanel;
        cooldownDailyRewardPresenter.OnClickToActivatedButton += sceneRoot.OpenDailyRewardPanel;

        dailyBonusPresenter.OnActivateSpin += cooldownDailyBonusPresenter.ActivateCooldown;
        cooldownDailyBonusPresenter.OnClickToActivatedButton += sceneRoot.OpenDailyBonusPanel;

        cooldownDailyBonusPresenter.OnUnvailable += dailyBonusPresenter.SetUnvailable;
        cooldownDailyBonusPresenter.OnAvailable += dailyBonusPresenter.SetAvailable;

        dailyRewardPresenter.OnGetDailyReward_Count += bankPresenter.SendMoney;
        dailyBonusPresenter.OnGetBonus += bankPresenter.SendMoney;
    }

    private void DeactivateEvents()
    {
        dailyRewardPresenter.OnGetDailyReward -= cooldownDailyRewardPresenter.ActivateCooldown;
        cooldownDailyRewardPresenter.OnClickToActivatedButton -= sceneRoot.OpenDailyRewardPanel;

        dailyBonusPresenter.OnActivateSpin -= cooldownDailyBonusPresenter.ActivateCooldown;
        cooldownDailyBonusPresenter.OnClickToActivatedButton -= sceneRoot.OpenDailyBonusPanel;

        cooldownDailyBonusPresenter.OnUnvailable -= dailyBonusPresenter.SetUnvailable;
        cooldownDailyBonusPresenter.OnAvailable -= dailyBonusPresenter.SetAvailable;

        dailyRewardPresenter.OnGetDailyReward_Count -= bankPresenter.SendMoney;
        dailyBonusPresenter.OnGetBonus -= bankPresenter.SendMoney;
    }

    private void Dispose()
    {
        DeactivateTransitionsSceneEvents();
        DeactivateEvents();
        sceneRoot.Deactivate();

        sceneRoot?.Dispose();
        particleEffectPresenter?.Dispose();
        soundPresenter?.Dispose();
        bankPresenter?.Dispose();
        dailyRewardPresenter?.Dispose();
        cooldownDailyRewardPresenter?.Dispose();
        cooldownDailyBonusPresenter?.Dispose();
        dailyBonusPresenter?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Input actions

    public event Action GoToMiniGame_Action;


    private void HandleGoToMiniGame()
    {
        Dispose();
        GoToMiniGame_Action?.Invoke();
    }

    #endregion
}
