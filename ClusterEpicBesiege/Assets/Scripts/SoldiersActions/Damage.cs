using UnityEngine;

public class Damage : MonoBehaviour, IDamageable
{
    SoldierController myContrl;

    private void Awake()
    {
        myContrl = transform.parent.GetComponent<SoldierController>();
    }

    public void DealDamage(float damage)
    {
        if (myContrl.SoldierInterface.Health > 0f) myContrl.SoldierInterface.Health -= damage;
        else myContrl.Death();
        print(myContrl.SoldierInterface.Health);
    }
}

