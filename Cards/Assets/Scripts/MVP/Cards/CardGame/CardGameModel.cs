using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameModel
{
    public event Action<bool> OnChooseChance_Values;
    public event Action OnChooseChance;

    public event Action OnActivate;
    public event Action OnDeactivate;

    public event Action OnChooseIncrease;
    public event Action OnChooseDecrease;

    public event Action OnReset;

    private bool chanceIncrease;

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public void Activate()
    {
        OnActivate?.Invoke();
    }

    public void Deactivate()
    {
        OnDeactivate?.Invoke();
    }

    public void Increase()
    {
        chanceIncrease = true;
        OnChooseIncrease?.Invoke();
    }

    public void Decrease()
    {
        chanceIncrease = false;
        OnChooseDecrease?.Invoke();
    }

    public void SubmitChance()
    {
        OnChooseChance?.Invoke();
        OnChooseChance_Values?.Invoke(chanceIncrease);
    }

    public void Reset()
    {
        OnReset?.Invoke();
    }
}
