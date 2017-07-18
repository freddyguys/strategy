using System.Collections;
using UnityEngine;

public class Airstrip : MonoBehaviour, IWeapon
{
    public GameObject planePrefab;
    public Transform airstrip;
    private SoldierController myContrl;
    private float angle = 65f;
    private float gravity = 20f;

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
        GameObject newPlane = Instantiate(planePrefab, airstrip.position, airstrip.rotation);
        float targetDistance = Vector3.Distance(airstrip.position, TargetPos);
        float projectileVelocity = targetDistance / (Mathf.Sin(2f * angle * Mathf.Deg2Rad) / gravity);
        newPlane.GetComponent<Plane>().AttackPower = myContrl.SoldierInterface.AttackPower;
        newPlane.GetComponent<Plane>().EnemyTeamTag = myContrl.EnemyTeamTag;
        newPlane.GetComponent<Plane>().DestinationPoint = TargetPos;
        float Vx = Mathf.Sqrt(projectileVelocity) * Mathf.Cos(angle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectileVelocity) * Mathf.Sin(angle * Mathf.Deg2Rad);
        float flightDuration = targetDistance / Vx;
        newPlane.transform.rotation = Quaternion.LookRotation(TargetPos - newPlane.transform.position);
        float elapseTime = 0;
        while (elapseTime < flightDuration)
        {
            newPlane.transform.Translate(0, (Vy - (gravity * elapseTime)) * Time.deltaTime, Vx * Time.deltaTime);
            elapseTime += Time.deltaTime;
            yield return null;
        }
    }
}
