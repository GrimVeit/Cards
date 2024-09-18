using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCardSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private CardValues cardValues;
    [SerializeField] private UIBigCardSceneRoot sceneRootPrefab;

    private UIBigCardSceneRoot sceneRoot;
    private ViewContainer viewContainer;

    private CardMovePresenter cardDraggingPresenter;
    private CardDroppingPresenter cardDroppingPresenter;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = Instantiate(sceneRootPrefab);
        sceneRoot.Initialize();
        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        cardDraggingPresenter = new CardMovePresenter(new CardMoveModel(), viewContainer.GetView<CardMoveView>());
        cardDraggingPresenter.Initialize();

        cardDroppingPresenter = new CardDroppingPresenter(new CardDroppingModel(cardValues), viewContainer.GetView<CardDroppingView>());
        cardDroppingPresenter.Initialize();

        cardDraggingPresenter.Activate();

        ActivateActions();
    }

    private void ActivateActions()
    {
        cardDraggingPresenter.OnStartMove += cardDroppingPresenter.Start;
        cardDraggingPresenter.OnStopMove += cardDroppingPresenter.Stop;

        cardDroppingPresenter.OnGetCard += cardDraggingPresenter.Deactivate;
    }

    private void DeactivateActions()
    {
        cardDraggingPresenter.OnStartMove -= cardDroppingPresenter.Start;
        cardDraggingPresenter.OnStopMove -= cardDroppingPresenter.Stop;

        cardDroppingPresenter.OnGetCard -= cardDraggingPresenter.Deactivate;
    }

    private void Dispose()
    {
        DeactivateActions();
        //sceneRoot?.Deactivate();

        //sceneRoot?.Dispose();
        cardDraggingPresenter?.Dispose();
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
