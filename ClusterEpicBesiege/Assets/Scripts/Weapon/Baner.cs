using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baner : MonoBehaviour, IWeapon
{
    Animator animator;
    SoldierController myContrl;

    private void Awake()
    {
        animator = transform.parent.parent.GetComponent<Animator>();
        myContrl = transform.parent.GetComponent<SoldierController>();
    }
    public void WeaponAttack()
    {
        myContrl.TargetContr.DamageInterface.DealDamage(myContrl.SoldierInterface.AttackPower);
    }
    // add Animation!!
}
