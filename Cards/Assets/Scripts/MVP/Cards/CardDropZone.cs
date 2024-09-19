using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDropZone : MonoBehaviour
{
    public event Action OnSpawnCard;
    public void SpawnCard()
    {
        OnSpawnCard?.Invoke();
    }
}
