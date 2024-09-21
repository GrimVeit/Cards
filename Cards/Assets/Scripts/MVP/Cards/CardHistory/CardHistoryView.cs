using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHistoryView : View
{
    public event Action OnClickToLeftScroll;
    public event Action OnClickToRightScroll;

    [SerializeField] private Button leftScrollButton;
    [SerializeField] private Button rightScrollButton;
    [SerializeField] private Transform transformStartSpawn;
    [SerializeField] private Transform transformEndSpawn;

    [SerializeField] private CardComboView cardComboViewPrefab;
    [SerializeField] private Transform canvas;
    [SerializeField] private Transform content;

    private Tween moveTween;

    public void Initialize()
    {
        leftScrollButton.onClick.AddListener(HandlerClickToLeftScroll);
        rightScrollButton.onClick.AddListener(HandlerClickToRightScroll);
    }

    public void Dispose()
    {
        leftScrollButton.onClick.RemoveListener(HandlerClickToLeftScroll);
        rightScrollButton.onClick.RemoveListener(HandlerClickToRightScroll);
    }

    public void AddCardCombo(CardValue leftCardValue, CardValue rightCardValue)
    {
        CardComboView cardComboView = Instantiate(cardComboViewPrefab, canvas);
        cardComboView.SetData(leftCardValue, rightCardValue);
        cardComboView.transform.SetPositionAndRotation(transformStartSpawn.position, cardComboViewPrefab.transform.rotation);

        cardComboView.transform.DOMove(transformEndSpawn.position, 0.2f).OnComplete(() => AddToScroll(cardComboView));
        cardComboView.transform.DOScale(Vector3.one, 0.2f);

    }

    private void AddToScroll(CardComboView cardComboView)
    {
        cardComboView.transform.SetParent(content);
        cardComboView.transform.SetSiblingIndex(0);
    }

    public void Clear()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    #region Input

    private void HandlerClickToLeftScroll()
    {
        OnClickToLeftScroll?.Invoke();
    }

    private void HandlerClickToRightScroll()
    {
        OnClickToRightScroll?.Invoke();
    }

    #endregion
}
