using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotTEMP : MonoBehaviour, IDamageable
{
    private float health = 100f;
    private bool isAlive = true;
    public bool IsAlive { get { return isAlive; } }

    public void DealDamage(float damage)
    {
        if (health > 0)
        { health -= damage; }
        else { isAlive = false; Destroy(gameObject, 1f); }
    }

}
