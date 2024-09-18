using System;

public class CardDroppingModel
{
    public event Action<CardValue> OnGetCard_Values;
    public event Action OnGetCard;

    public event Action OnStart;
    public event Action OnStop;

    private CardValues cardValues;

    public CardDroppingModel(CardValues cardValues)
    {
        this.cardValues = cardValues;
    }

    public void Start()
    {
        OnStart?.Invoke();
    }

    public void Stop()
    {
        OnStop?.Invoke();
    }

    public void GetCard()
    {
        CardValue cardValue = cardValues.GetRandom();

        OnGetCard_Values?.Invoke(cardValue);
        OnGetCard?.Invoke();
    }
}
