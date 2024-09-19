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

    private CardBetPresenter cardUserBetPresenter;

    private CardMovePresenter cardUserMovePresenter;
    private CardMoveAIPresenter cardMoveAIPresenter;

    private CardSpawnerPresenter cardUserSpawnerPresenter;
    private CardSpawnerPresenter cardAISpawnerPresenter;

    private CardHighlightPresenter cardUserHighlightPresenter;
    private CardHighlightPresenter cardAIHighlightPresenter;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = Instantiate(sceneRootPrefab);
        sceneRoot.Initialize();
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

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

        cardUserMovePresenter.Activate();

        ActivateActions();
    }

    private void ActivateActions()
    {
        cardUserMovePresenter.OnStartMove += cardUserHighlightPresenter.ActivateChooseHighlight;
        cardUserMovePresenter.OnStopMove += cardUserHighlightPresenter.DeactivateChooseHighlight;

        cardUserSpawnerPresenter.OnSpawnCard += cardUserMovePresenter.Deactivate;
        cardUserSpawnerPresenter.OnSpawnCard += cardUserBetPresenter.Activate;

        cardUserBetPresenter.OnSubmitBet += cardUserBetPresenter.Deactivate;
        cardUserBetPresenter.OnSubmitBet += cardMoveAIPresenter.Activate;

        cardMoveAIPresenter.OnStartMove += cardAIHighlightPresenter.ActivateChooseHighlight;
        cardMoveAIPresenter.OnEndMove += cardAIHighlightPresenter.DeactivateChooseHighlight;

        cardMoveAIPresenter.OnEndMove += cardAISpawnerPresenter.SpawnCard;

        cardAISpawnerPresenter.OnSpawnCard += cardMoveAIPresenter.Deactivate;
    }

    private void DeactivateActions()
    {

    }

    private void Dispose()
    {
        DeactivateActions();
        //sceneRoot?.Deactivate();

        //sceneRoot?.Dispose();
        cardUserMovePresenter?.Dispose();
        cardUserSpawnerPresenter?.Dispose();
        cardUserBetPresenter?.Dispose();
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
