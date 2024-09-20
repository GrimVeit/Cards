using System;

public class CardComparisionPresenter
{
    private CardComparisionModel cardComparisionModel;

    public CardComparisionPresenter(CardComparisionModel cardComparisionModel)
    {
        this.cardComparisionModel = cardComparisionModel;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    #region Input

    public void OnSpawnedCard(CardValue cardValue)
    {
        cardComparisionModel.OnCardSpawned(cardValue);
    }

    public event Action OnSuccessGame
    {
        add { cardComparisionModel.OnSuccessGame += value; }
        remove { cardComparisionModel.OnSuccessGame -= value; }
    }

    public event Action OnLoseGame
    {
        add { cardComparisionModel.OnLoseGame += value; }
        remove { cardComparisionModel.OnLoseGame -= value; }
    }

    #endregion
}
