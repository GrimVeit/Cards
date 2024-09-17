using System;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UIMainMenuRoot menuRootPrefab;

    private UIMainMenuRoot sceneRoot;
    private ViewContainer viewContainer;

    private WebViewPresenter privacyPolicyWebViewPresenter;
    private WebViewPresenter aboutWebViewPresenter;

    private ParticleEffectPresenter particleEffectPresenter;
    private SoundPresenter soundPresenter;
    private BankPresenter bankPresenter;
    private DailyRewardPresenter dailyRewardPresenter;
    private DailyBonusPresenter dailyBonusPresenter;
    private ShopPresenter shopPresenter;

    private CooldownPresenter cooldownDailyRewardPresenter;
    private CooldownPresenter cooldownDailyBonusPresenter;


    private ISoundProvider soundProvider;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = Instantiate(menuRootPrefab);
 
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        cooldownDailyRewardPresenter = new CooldownPresenter
                    (new CooldownModel(PlayerPrefsKeys.NEXT_DAILY_REWARD_TIME, TimeSpan.FromSeconds(20)),
                    viewContainer.GetView<CooldownView>("DailyReward"));
        cooldownDailyRewardPresenter.Initialize();

        cooldownDailyBonusPresenter = new CooldownPresenter
            (new CooldownModel(PlayerPrefsKeys.NEXT_DAILY_BONUS_TIME, TimeSpan.FromSeconds(25)),
            viewContainer.GetView<CooldownView>("DailyBonus"));
        cooldownDailyBonusPresenter.Initialize();

        privacyPolicyWebViewPresenter = new WebViewPresenter
            (new WebViewModel("https://fgo.today/policy"),
            viewContainer.GetView<WebViewView>("PrivacyPolicy"));
        privacyPolicyWebViewPresenter.Initialize();

        aboutWebViewPresenter = new WebViewPresenter
            (new WebViewModel("https://fgo.today/about"),
            viewContainer.GetView<WebViewView>("About"));
        aboutWebViewPresenter.Initialize();

        soundPresenter = new SoundPresenter
            (new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS),
            viewContainer.GetView<SoundView>());
        soundPresenter.Initialize();

        particleEffectPresenter = new ParticleEffectPresenter
            (new ParticleEffectModel(),
            viewContainer.GetView<ParticleEffectView>());
        particleEffectPresenter.Initialize();

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        bankPresenter.Initialize();

        dailyRewardPresenter = new DailyRewardPresenter
            (new DailyRewardModel(soundPresenter, particleEffectPresenter),
            viewContainer.GetView<DailyRewardView>());
        dailyRewardPresenter.Initialize();

        dailyBonusPresenter = new DailyBonusPresenter(new DailyBonusModel(), viewContainer.GetView<DailyBonusView>());
        dailyBonusPresenter.Initialize();

        shopPresenter = new ShopPresenter(new ShopModel(), viewContainer.GetView<ShopView>());
        shopPresenter.Initialize();

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
        sceneRoot.GoToSlots1_Action += HandleGoToSlots1;
        sceneRoot.GoToSlots2_Action += HandleGoToSlots2;
        sceneRoot.GoToSlots3_Action += HandleGoToSlots3;
        sceneRoot.GoToMiniGame_Action += HandleGoToMiniGame;

        sceneRoot.OnActivatePrivacyPolicyPanel += privacyPolicyWebViewPresenter.Load;
        sceneRoot.OnActivateAboutPanel += aboutWebViewPresenter.Load;

        privacyPolicyWebViewPresenter.OnHidePage += sceneRoot.OpenSettingsPanel;
        aboutWebViewPresenter.OnHidePage += sceneRoot.OpenSettingsPanel;
    }

    private void DeactivateTransitionsSceneEvents()
    {
        sceneRoot.GoToSlots1_Action -= HandleGoToSlots1;
        sceneRoot.GoToSlots2_Action -= HandleGoToSlots2;
        sceneRoot.GoToSlots3_Action -= HandleGoToSlots3;
        sceneRoot.GoToMiniGame_Action -= HandleGoToMiniGame;

        sceneRoot.OnActivatePrivacyPolicyPanel -= privacyPolicyWebViewPresenter.Load;
        sceneRoot.OnActivateAboutPanel -= aboutWebViewPresenter.Load;

        privacyPolicyWebViewPresenter.OnHidePage -= sceneRoot.OpenSettingsPanel;
        aboutWebViewPresenter.OnHidePage -= sceneRoot.OpenSettingsPanel;
    }

    private void ActivateEvents()
    {
        dailyRewardPresenter.OnGetDailyReward += cooldownDailyRewardPresenter.ActivateCooldown;
        cooldownDailyRewardPresenter.OnClickToActivatedButton += sceneRoot.OpenDailyRewardPanel;
        cooldownDailyRewardPresenter.OnClickToDeactivatedButton += sceneRoot.OpenCooldownDailyRewardPanel;
        cooldownDailyRewardPresenter.OnAvailable += sceneRoot.CloseCooldownDailyRewardPanel;

        dailyBonusPresenter.OnActivateSpin += cooldownDailyBonusPresenter.ActivateCooldown;
        cooldownDailyBonusPresenter.OnClickToActivatedButton += sceneRoot.OpenDailyBonusPanel;
        cooldownDailyBonusPresenter.OnClickToDeactivatedButton += sceneRoot.OpenCooldownDailyBonusPanel;
        cooldownDailyBonusPresenter.OnAvailable += sceneRoot.CloseCooldownDailyBonusPanel;

        cooldownDailyBonusPresenter.OnUnvailable += dailyBonusPresenter.SetUnvailable;
        cooldownDailyBonusPresenter.OnAvailable += dailyBonusPresenter.SetAvailable;

        dailyRewardPresenter.OnGetDailyReward_Count += bankPresenter.SendMoney;
        dailyBonusPresenter.OnGetBonus += bankPresenter.SendMoney;
    }

    private void DeactivateEvents()
    {
        dailyRewardPresenter.OnGetDailyReward -= cooldownDailyRewardPresenter.ActivateCooldown;
        cooldownDailyRewardPresenter.OnClickToActivatedButton -= sceneRoot.OpenDailyRewardPanel;
        cooldownDailyRewardPresenter.OnClickToDeactivatedButton -= sceneRoot.OpenCooldownDailyRewardPanel;
        cooldownDailyRewardPresenter.OnAvailable -= sceneRoot.CloseCooldownDailyRewardPanel;

        dailyBonusPresenter.OnActivateSpin -= cooldownDailyBonusPresenter.ActivateCooldown;
        cooldownDailyBonusPresenter.OnClickToActivatedButton -= sceneRoot.OpenDailyBonusPanel;
        cooldownDailyBonusPresenter.OnClickToDeactivatedButton -= sceneRoot.OpenCooldownDailyBonusPanel;
        cooldownDailyBonusPresenter.OnAvailable -= sceneRoot.CloseCooldownDailyBonusPanel;

        cooldownDailyBonusPresenter.OnUnvailable -= dailyBonusPresenter.SetUnvailable;
        cooldownDailyBonusPresenter.OnAvailable -= dailyBonusPresenter.SetAvailable;

        dailyRewardPresenter.OnGetDailyReward_Count -= bankPresenter.SendMoney;
        dailyBonusPresenter.OnGetBonus -= bankPresenter.SendMoney;
    }

    private void OnDestroy()
    {
        Dispose();
    }

    private void Dispose()
    {
        DeactivateTransitionsSceneEvents();
        DeactivateEvents();
        sceneRoot.Deactivate();

        sceneRoot?.Dispose();
        privacyPolicyWebViewPresenter?.Dispose();
        aboutWebViewPresenter?.Dispose();
        particleEffectPresenter?.Dispose();
        soundPresenter?.Dispose();
        bankPresenter?.Dispose();
        dailyRewardPresenter?.Dispose();
        cooldownDailyRewardPresenter?.Dispose();
        cooldownDailyBonusPresenter?.Dispose();
        dailyBonusPresenter?.Dispose();
        shopPresenter?.Dispose();
    }

    #region Input actions

    public event Action GoToSlots1_Action;
    public event Action GoToSlots2_Action;
    public event Action GoToSlots3_Action;
    public event Action GoToMiniGame_Action;

    private void HandleGoToSlots1()
    {
        Dispose();
        GoToSlots1_Action?.Invoke();
    }

    private void HandleGoToSlots2()
    {
        Dispose();
        GoToSlots2_Action?.Invoke();
    }

    private void HandleGoToSlots3()
    {
        Dispose();
        GoToSlots3_Action?.Invoke();
    }

    private void HandleGoToMiniGame()
    {
        Dispose();
        GoToMiniGame_Action?.Invoke();
    }

    #endregion
}
