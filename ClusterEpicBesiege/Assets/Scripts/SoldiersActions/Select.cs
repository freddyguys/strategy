using System;
using UnityEngine;

public class Select : MonoBehaviour, ISelectable
{
    SpriteRenderer spriteIndicator;

    private void Awake()
    {
        spriteIndicator = GetComponent<SpriteRenderer>();
    }

    public bool Indicator
    {
        get { return spriteIndicator.enabled; }
        set { spriteIndicator.enabled = value; }
    }
}
