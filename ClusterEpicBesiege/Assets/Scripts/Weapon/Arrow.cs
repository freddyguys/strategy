using UnityEngine;

public class Arrow : MonoBehaviour
{
    bool oneShot = false;
    public float attackPower;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "BadGuy" && !oneShot)
        {
            oneShot = true;
            collision.transform.GetChild(0).GetComponent<SoldierController>().DamageInterface.DealDamage(attackPower);
        }
    }


}
