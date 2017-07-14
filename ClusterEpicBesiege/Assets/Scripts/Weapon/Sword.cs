using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    SoldierController myContrl;

    private void Awake()
    {
        myContrl = transform.parent.GetComponent<SoldierController>();
    }

    public void WeaponAttack()
    {
        myContrl.Animator.SetBool("attack", true);
        Invoke("Hit", 0.5f);
    }

    public void Hit()
    {
        myContrl.TargetContr.DamageInterface.DealDamage(myContrl.SoldierInterface.AttackPower);
        myContrl.Animator.SetBool("attack", false);
        myContrl.TargetContr = null;
        myContrl.IsFind = true;
        myContrl.IsAttack = false;
    }

}
