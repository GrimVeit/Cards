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
    private ITutorialProvider tutorialProvider;

    public CardMoveModel(ITutorialProvider tutorialProvider)
    {
        this.tutorialProvider = tutorialProvider;
    }


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
                if(cardDropZone.Owner == Owner.User)
                {
                    cardDropZone.SpawnCard();
                }
            }
        }

        OnEndMove?.Invoke();
    }

    public void Teleport()
    {
        OnTeleporting?.Invoke();
    }

    public void Activate()
    {
        isActive = true;

        if(tutorialProvider.IsActiveTutorial())
            tutorialProvider.ActivateTutorial("TakeCard");
    }


    public void Deactivate()
    {
        isActive = false;

        if (tutorialProvider.IsActiveTutorial())
            tutorialProvider.DeactivateTutorial("TakeCard");
    }
}
