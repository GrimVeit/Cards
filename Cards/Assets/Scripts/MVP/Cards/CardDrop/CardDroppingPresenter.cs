using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDroppingPresenter
{
    private CardDroppingModel cardDroppingModel;
    private CardDroppingView cardDroppingView;

    public CardDroppingPresenter(CardDroppingModel cardDroppingModel, CardDroppingView cardDroppingView)
    {
        this.cardDroppingModel = cardDroppingModel;
        this.cardDroppingView = cardDroppingView;
    }

    public void Initialize()
    {
        ActivateEvents();

        cardDroppingView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        cardDroppingView.Dispose();
    }

    private void ActivateEvents()
    {
        cardDroppingView.OnGetCard += cardDroppingModel.GetCard;

        cardDroppingModel.OnGetCard_Values += cardDroppingView.SpawnCard;
        cardDroppingModel.OnStart += cardDroppingView.Activate;
        cardDroppingModel.OnStop += cardDroppingView.Deactivate;
    }

    private void DeactivateEvents()
    {
        cardDroppingView.OnGetCard -= cardDroppingModel.GetCard;

        cardDroppingModel.OnStart -= cardDroppingView.Activate;
        cardDroppingModel.OnStop -= cardDroppingView.Deactivate;
    }

    #region Input

    public event Action<CardValue> OnGetCard_Values
    {
        add { cardDroppingModel.OnGetCard_Values += value; }
        remove { cardDroppingModel.OnGetCard_Values -= value; }
    }

    public event Action OnGetCard
    {
        add { cardDroppingModel.OnGetCard += value; }
        remove { cardDroppingModel.OnGetCard -= value; }
    }

    public void Start()
    {
        cardDroppingModel.Start();
    }

    public void Stop()
    {
        cardDroppingModel.Stop();
    }

    #endregion
}
