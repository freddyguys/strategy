using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    public GameObject arrowPrefab;
    public Transform bowTransform;
    private SoldierController myContrl;
    private float speed;

    private void Awake()
    {
        myContrl = transform.parent.GetComponent<SoldierController>();
    }

    public void WeaponAttack()
    {
        //GameObject newArrow = Instantiate(arrowPrefab, bowTransform.position, bowTransform.rotation);
        StartCoroutine(SimulateProjectile());

        //Rigidbody rb = newArrow.GetComponent<Rigidbody>();
        //newArrow.GetComponent<Arrow>().attackPower = myContrl.SoldierInterface.AttackPower;
        //newArrow.GetComponent<Arrow>().pointTR = myContrl.TargetContr.transform;
        //Vector3 direction = myContrl.TargetContr.GetPositions() - bowTransform.position;
        //float distance = Vector3.Distance(myContrl.GetPositions(), myContrl.TargetContr.GetPositions());
        //if (distance < 3f) speed = 4f;
        //else speed = distance * 1.22f;

        // rb.velocity = -bowTransform.right.normalized * speed;
        //rb.velocity = direction.normalized * speed;


        Invoke("Reload", 1.3f);
    }

    public void Reload()
    {
        myContrl.IsAttack = false;
        myContrl.IsFind = true;
    }



    IEnumerator SimulateProjectile()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject newArrow = Instantiate(arrowPrefab, bowTransform.position, bowTransform.rotation);
        float targetDistance = Vector3.Distance(bowTransform.position, myContrl.TargetContr.GetPositions());
        float projectileVelocity = targetDistance / (Mathf.Sin(2f * 45f * Mathf.Deg2Rad) / 9.8f);

        float Vx = Mathf.Sqrt(projectileVelocity) * Mathf.Cos(45f * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectileVelocity) * Mathf.Sin(45f * Mathf.Deg2Rad);
        float flightDuration = targetDistance / Vx;
        newArrow.transform.rotation = Quaternion.LookRotation(myContrl.TargetContr.GetPositions() - newArrow.transform.position);
        float elapseTime = 0;

        while (elapseTime < flightDuration)
        {
            newArrow.transform.Translate(0, (Vy - (9.8f * elapseTime)) * Time.deltaTime, Vx * Time.deltaTime);
            elapseTime += Time.deltaTime;
            yield return null;
        }
    }
}
