using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMoveModel
{
    public event Action OnStartMove;
    public event Action<Vector2> OnMove;
    public event Action OnEndMove;
    public event Action OnTeleporting;

    private bool isActive = true;


    public void StartMove()
    {
        if (!isActive) return;

        OnStartMove?.Invoke();
    }

    public void Move(Vector2 vector)
    {
        if (!isActive) return;

        OnMove?.Invoke(vector);
    }

    public void EndMove(PointerEventData pointerEventData)
    {
        if (!isActive) return;

        if (pointerEventData.pointerEnter != null)
        {
            if(pointerEventData.pointerEnter.TryGetComponent(out CardDropZone cardDropZone))
            {
                cardDropZone.SpawnCard();
                OnTeleporting?.Invoke();
            }
        }

        OnEndMove?.Invoke();
    }

    public void Activate()
    {
        isActive = true;
    }


    public void Deactivate()
    {
        isActive = false;
    }
}
