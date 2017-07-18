using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baner : MonoBehaviour, IWeapon
{
    private float actionRadius = 3f;

    public void WeaponAttack(SoldierController targetContrl)
    {
        FindRecruit();
    }

    public void FindRecruit()
    {
        List<SoldierController> soldiers = new List<SoldierController>();
        foreach (Collider col in Physics.OverlapSphere(transform.position, actionRadius))
        {
            if (col.tag == "GoodGuy" && col.transform.GetChild(0).GetComponent<SoldierController>().IsAlive) soldiers.Add(col.transform.GetChild(0).GetComponent<SoldierController>());
        }
        StartCoroutine(Recruitment(soldiers));
    }

    IEnumerator Recruitment(List<SoldierController> soldiers)
    {
        yield return new WaitForSecondsRealtime(5f);
        List<SoldierController> newSoldiers = new List<SoldierController>();
        foreach (Collider col in Physics.OverlapSphere(transform.position, actionRadius))
        {
            if (col.tag == "GoodGuy" && col.transform.GetChild(0).GetComponent<SoldierController>().IsAlive) newSoldiers.Add(col.transform.GetChild(0).GetComponent<SoldierController>());
        }
        foreach (SoldierController soldier in soldiers)
        {
            if (newSoldiers.Contains(soldier)) soldier.SetTeam(TeamTag.BadGuy);
        }
    }
}
