using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    SoldierController myContrl;

    private void Awake()
    {
        myContrl = transform.parent.GetComponent<SoldierController>();
    }

    public void WeaponAttack(SoldierController targetContrl)
    {
        myContrl.Animator.SetBool("attack", true);
        Invoke("Hit", 0.5f);
    }

    public void Hit()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f))
        {
            if (hit.collider.tag == myContrl.EnemyTeamTag.ToString()) hit.collider.transform.GetChild(0).GetComponent<SoldierController>().DamageInterface.DealDamage(myContrl.SoldierInterface.AttackPower);
        }
        myContrl.Animator.SetBool("attack", false);
        myContrl.TargetContr = null;
        myContrl.IsFind = true;
        myContrl.IsAttack = false;
    }
}
