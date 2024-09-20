using System;
using System.Collections.Generic;

public class CardComparisionModel
{
    private List<CardValue> cards = new List<CardValue>();

    public event Action OnSuccessGame;
    public event Action OnLoseGame;

    private bool resultGame;
    private bool userCompareResult;

    public void OnCardSpawned(CardValue cardValue)
    {
        cards.Add(cardValue);

        if(cards.Count == 2)
        {
            resultGame = cards[0].CardNominal > cards[1].CardNominal;
            if(resultGame == userCompareResult)
            {
                OnSuccessGame?.Invoke();
            }
            else
            {
                OnLoseGame?.Invoke();
            }
            UnityEngine.Debug.Log(cards[0].CardNominal + "//" + cards[1].CardNominal + "//" + (cards[0].CardNominal > cards[1].CardNominal));
            cards.Clear();
        }
    }

    public void UserCompare(bool result)
    {
        userCompareResult = result;
    }
}
