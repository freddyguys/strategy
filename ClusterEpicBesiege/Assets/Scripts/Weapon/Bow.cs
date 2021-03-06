﻿using System.Collections;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    public GameObject arrowPrefab;
    public Transform bowTransform;
    private SoldierController myContrl;
    private float angle = 30f;
    private float gravity = 25f;

    private void Awake()
    {
        myContrl = transform.parent.GetComponent<SoldierController>();
    }

    public void WeaponAttack(SoldierController targetContrl)
    {
        StartCoroutine(SimulateProjectile(targetContrl));
    }

    IEnumerator SimulateProjectile(SoldierController soldierContr)
    {
        Vector3 TargetPos = soldierContr.GetPositions();
        TargetPos.y = 0f;
        GameObject newArrow = Instantiate(arrowPrefab, bowTransform.position, bowTransform.rotation);
        float targetDistance = Vector3.Distance(bowTransform.position, TargetPos);
        float projectileVelocity = targetDistance / (Mathf.Sin(2f * angle * Mathf.Deg2Rad) / gravity);
        newArrow.GetComponent<Arrow>().AttackPower = myContrl.SoldierInterface.AttackPower;
        newArrow.GetComponent<Arrow>().EnemyTeamTag = myContrl.EnemyTeamTag;
        newArrow.GetComponent<Arrow>().DestinationPoint = TargetPos;
        float Vx = Mathf.Sqrt(projectileVelocity) * Mathf.Cos(angle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectileVelocity) * Mathf.Sin(angle * Mathf.Deg2Rad);
        float flightDuration = targetDistance / Vx;
        newArrow.transform.rotation = Quaternion.LookRotation(TargetPos - newArrow.transform.position);
        float elapseTime = 0;
        while (elapseTime < flightDuration)
        {
            newArrow.transform.Translate(0, (Vy - (gravity * elapseTime)) * Time.deltaTime, Vx * Time.deltaTime);
            elapseTime += Time.deltaTime;
            yield return null;
        }
    }
}
