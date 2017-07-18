using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour, IAttackable
{
    SoldierController myContrl;
    bool canAttack = true;

    private void Awake()
    {
        myContrl = transform.parent.GetComponent<SoldierController>();
    }

    public void AttackTarget(SoldierController targetContrl)
    {
        myContrl.IsFind = false;
        myContrl.TargetContr = targetContrl;

        if (Vector3.Distance(targetContrl.GetPositions(), myContrl.GetPositions()) > myContrl.SoldierInterface.AttackRange)
        {
            myContrl.IsMoveToTarget = true;
            myContrl.MoveInterface.MoveTo(targetContrl.GetPositions());
        }
        else
        {
            if (canAttack)
            {
                canAttack = false;
                myContrl.StopSoldier();
                myContrl.IsAttack = true;
                myContrl.WeaponInterface.WeaponAttack(targetContrl);
                StartCoroutine(Colldown());
            }
        }
    }

    IEnumerator Colldown()
    {
        yield return new WaitForSecondsRealtime(myContrl.SoldierInterface.Cooldown);
        myContrl.IsAttack = false;
        canAttack = true;
        if (myContrl.TargetContr == null) myContrl.IsFind = true;
        else AttackTarget(myContrl.TargetContr);

    }
}
