using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private bool isAttack = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "badGuy" && !isAttack)
        {
            isAttack = true;
            collision.collider.GetComponent<IDamageable>().DealDamage(12f);

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "badGuy" && isAttack)
        {
            isAttack = false;
        }
    }
}
