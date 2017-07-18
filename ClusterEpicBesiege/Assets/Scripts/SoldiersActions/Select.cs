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

    public void ChangeColor(TeamTag tag)
    {
        spriteIndicator.color = tag == TeamTag.BadGuy ? new Color(255f / 255f, 96f / 255f, 96f / 255f, 159f / 255f) : new Color(175f / 255f, 96f / 236f, 183f / 255f, 159f / 255f);
    }
}
