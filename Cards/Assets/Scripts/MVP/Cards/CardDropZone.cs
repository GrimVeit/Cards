using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDropZone : MonoBehaviour
{
    public event Action OnGetCard;
    public void GetCard()
    {
        OnGetCard?.Invoke();
    }
}
