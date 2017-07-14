using UnityEngine;

public class Attack : MonoBehaviour, IAttackable
{
    SoldierController myContrl;

    private void Awake()
    {
        myContrl = transform.parent.GetComponent<SoldierController>();
    }

    public void AttackTarget(SoldierController targetContrl)
    {
        if (!myContrl.IsAttack)
        {
            myContrl.TargetContr = targetContrl;
            myContrl.IsFind = false;
            if (Vector3.Distance(targetContrl.GetPositions(), myContrl.GetPositions()) > myContrl.SoldierInterface.AttackRange)
            {
                myContrl.MoveInterface.MoveTo(targetContrl.GetPositions());
                myContrl.IsMoveToTarget = true;
            }
            else
            {
                myContrl.StopSoldier();
                myContrl.IsAttack = true;
                myContrl.WeaponInterface.WeaponAttack();
            }
        }
    }




}
