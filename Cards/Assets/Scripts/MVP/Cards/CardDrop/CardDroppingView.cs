using DG.Tweening;
using System;
using UnityEngine;

public class CardDroppingView : View
{
    public event Action OnGetCard;

    [SerializeField] private CardView cardViewPrefab;
    [SerializeField] private CardDropZone cardDropZone;
    [SerializeField] private Transform dropCardObject;

    [SerializeField] private Transform cardViewParent;
    private CardView currentCardView;

    private Vector3 normalScale;
    private Tween scaleTween;

    public void Initialize()
    {
        normalScale = dropCardObject.localScale;

        cardDropZone.OnGetCard += HandlerGetCard;
    }

    public void Dispose()
    {
        cardDropZone.OnGetCard -= HandlerGetCard;
    }

    public void Activate()
    {
        scaleTween = dropCardObject.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.6f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }

    public void Deactivate()
    {
        if (scaleTween != null)
            scaleTween.Kill();
        dropCardObject.localScale = normalScale;
    }

    public void SpawnCard(CardValue cardValue)
    {
        currentCardView = Instantiate(cardViewPrefab, cardViewParent);
        currentCardView.transform.SetLocalPositionAndRotation(Vector3.zero, cardViewPrefab.transform.rotation);
        currentCardView.SetData(cardValue);
    }

    private void HandlerGetCard()
    {
        OnGetCard?.Invoke();
    }
}
