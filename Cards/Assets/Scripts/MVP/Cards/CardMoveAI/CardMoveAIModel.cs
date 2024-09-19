using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMoveAIModel
{
    public event Action OnStartMove;
    public event Action OnEndMove;
    public event Action OnTeleporting;

    public void EndMove()
    {
        OnEndMove?.Invoke();
    }

    public void Activate()
    {
        OnStartMove?.Invoke();
    }

    public void Deactivate()
    {
        OnTeleporting?.Invoke();
    }
}
