using UnityEngine;

public interface IDamageable
{
    void DealDamage(float damage);
    bool IsAlive { get; }
}
