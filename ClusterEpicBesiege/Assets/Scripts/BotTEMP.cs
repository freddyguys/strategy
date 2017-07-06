using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotTEMP : MonoBehaviour, IDamageable
{
    private float health = 100f;
    public void dealDamage(float damage)
    {
        if (health > 0)
        { health -= damage; print(health); }
        else Destroy(gameObject);
    }

}
