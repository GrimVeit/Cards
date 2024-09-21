using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHistoryModel
{
    public event Action<CardValue, CardValue> OnAddCardCombo;
    public event Action OnClearHistory;
    
    public void AddCardCombo(CardValue leftCard, CardValue rightCard)
    {
        OnAddCardCombo?.Invoke(leftCard, rightCard);
    }

    public void Clear()
    {
        OnClearHistory?.Invoke();
    }
}
