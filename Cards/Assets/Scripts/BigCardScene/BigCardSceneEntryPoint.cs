using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCardSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private CardValues cardValues;
    [SerializeField] private BetAmounts amounts;
    [SerializeField] private UIBigCardSceneRoot sceneRootPrefab;

    private UIBigCardSceneRoot sceneRoot;
    private ViewContainer viewContainer;

    private BankPresenter bankPresenter;

    private CardBetPresenter cardUserBetPresenter;

    private CardMovePresenter cardUserMovePresenter;
    private CardMoveAIPresenter cardMoveAIPresenter;

    private CardSpawnerPresenter cardUserSpawnerPresenter;
    private CardSpawnerPresenter cardAISpawnerPresenter;

    private CardHighlightPresenter cardUserHighlightPresenter;
    private CardHighlightPresenter cardAIHighlightPresenter;

    private CardGamePresenter cardGamePresenter;

    private CardComparisionPresenter cardComparisionPresenter;

    private CardGameWalletPresenter cardGameWalletPresenter;

    private CardHistoryPresenter cardHistoryPresenter;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = Instantiate(sceneRootPrefab);
        sceneRoot.Initialize();
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        bankPresenter.Initialize();

        cardUserMovePresenter = new CardMovePresenter(new CardMoveModel(), viewContainer.GetView<CardMoveView>());
        cardUserMovePresenter.Initialize();

        cardUserSpawnerPresenter = new CardSpawnerPresenter(new CardSpawnerModel(cardValues), viewContainer.GetView<CardSpawnerView>("User"));
        cardUserSpawnerPresenter.Initialize();

        cardAISpawnerPresenter = new CardSpawnerPresenter(new CardSpawnerModel(cardValues), viewContainer.GetView<CardSpawnerView>("AI"));
        cardAISpawnerPresenter.Initialize();

        cardUserBetPresenter = new CardBetPresenter(new CardBetModel(amounts), viewContainer.GetView<CardBetView>());
        cardUserBetPresenter.Initialize();

        cardMoveAIPresenter = new CardMoveAIPresenter(new CardMoveAIModel(), viewContainer.GetView<CardMoveAIView>());
        cardMoveAIPresenter.Initialize();

        cardUserHighlightPresenter = new CardHighlightPresenter(new CardHighlightModel(), viewContainer.GetView<CardHighlightView>("User"));
        cardUserHighlightPresenter.Initilize();

        cardAIHighlightPresenter = new CardHighlightPresenter(new CardHighlightModel(), viewContainer.GetView<CardHighlightView>("AI"));
        cardAIHighlightPresenter.Initilize();

        cardGamePresenter = new CardGamePresenter(new CardGameModel(), viewContainer.GetView<CardGameView>());
        cardGamePresenter.Initialize();

        cardComparisionPresenter = new CardComparisionPresenter(new CardComparisionModel());
        cardComparisionPresenter.Initialize();

        cardGameWalletPresenter = new CardGameWalletPresenter(new CardGameWalletModel(bankPresenter), viewContainer.GetView<CardGameWalletView>());
        cardGameWalletPresenter.Initialize();

        cardHistoryPresenter = new CardHistoryPresenter(new CardHistoryModel(), viewContainer.GetView<CardHistoryView>());
        cardHistoryPresenter.Initialize();

        cardUserMovePresenter.Activate();

        ActivateComparisedCardEvents();
        ActivateTransferPanelsEvents();
        ActivateActions();

        sceneRoot.Activate();
    }

    private void ActivateActions()
    {
        cardUserMovePresenter.OnStartMove += cardUserHighlightPresenter.ActivateChooseHighlight;
        cardUserMovePresenter.OnStopMove += cardUserHighlightPresenter.DeactivateChooseHighlight;

        cardUserSpawnerPresenter.OnSpawnCard += cardUserMovePresenter.Deactivate;
        cardUserSpawnerPresenter.OnSpawnCard += cardUserBetPresenter.Activate;
        cardUserSpawnerPresenter.OnSpawnCard += cardGamePresenter.Reset;

        cardUserBetPresenter.OnSubmitBet += cardUserBetPresenter.Deactivate;
        cardUserBetPresenter.OnSubmitBet += cardGamePresenter.Activate;
        cardUserBetPresenter.OnSubmitBet_Value += cardGameWalletPresenter.SetBet;

        cardGamePresenter.OnChooseChance += cardGamePresenter.Deactivate;
        cardGamePresenter.OnChooseChance += cardMoveAIPresenter.Activate;
        cardGamePresenter.OnChooseChance_Values += cardComparisionPresenter.UserCompare;

        cardMoveAIPresenter.OnStartMove += cardAIHighlightPresenter.ActivateChooseHighlight;
        cardMoveAIPresenter.OnEndMove += cardAIHighlightPresenter.DeactivateChooseHighlight;

        cardMoveAIPresenter.OnEndMove += cardAISpawnerPresenter.SpawnCard;

        cardAISpawnerPresenter.OnSpawnCard += cardMoveAIPresenter.Deactivate;
    }

    private void ActivateComparisedCardEvents()
    {
        cardUserSpawnerPresenter.OnSpawnCard_Values += cardComparisionPresenter.OnSpawnedCard;
        cardAISpawnerPresenter.OnSpawnCard_Values += cardComparisionPresenter.OnSpawnedCard;

        cardComparisionPresenter.OnSuccessGame += cardGameWalletPresenter.IncreaseMoney;
        cardComparisionPresenter.OnLoseGame += cardGameWalletPresenter.DecreaseMoney;
        cardComparisionPresenter.OnGetCards_Values += cardHistoryPresenter.AddCardComboHistory;

        cardComparisionPresenter.OnGetCards += cardUserSpawnerPresenter.DestroyCard;
        cardComparisionPresenter.OnGetCards += cardAISpawnerPresenter.DestroyCard;
        cardComparisionPresenter.OnGetCards += cardUserMovePresenter.Activate;
    }

    private void ActivateTransferPanelsEvents()
    {
        sceneRoot.OnClickToMoveWinningsButton += sceneRoot.OpenMoveWinningsPanel;
        sceneRoot.OnClickToBackFromMoveWinningsButton += sceneRoot.CloseMoveWinningsPanel;

        cardGameWalletPresenter.OnMoneySuccesTransitToBank += sceneRoot.CloseMoveWinningsPanel;
        cardGameWalletPresenter.OnMoneySuccesTransitToBank += sceneRoot.OpenMoveMoneyPanel;
        sceneRoot.OnClickToContinueGameButton += sceneRoot.CloseMoveMoneyPanel;
    }

    private void Dispose()
    {

    }

    #region Input Actions

    public event Action GoToMainMenu;

    private void HandleGoToMainMenu()
    {
        Dispose();
        GoToMainMenu?.Invoke();
    }

    #endregion
}
