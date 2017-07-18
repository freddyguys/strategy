using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plane : MonoBehaviour
{
    private float actionRadius = 3f;
    private bool isFly = true;
    private Transform plain;
    private Vector3 destinationPoint;
    private float attackPower;
    private TeamTag enemyTeamTag;

    public float AttackPower { set { attackPower = value; } }
    public TeamTag EnemyTeamTag { set { enemyTeamTag = value; } }
    public Vector3 DestinationPoint { set { destinationPoint = value; } }

    private void Awake()
    {
        plain = gameObject.transform.GetChild(0).GetComponent<Transform>();
        Destroy(gameObject, 8f);
    }

    private void Update()
    {
        if (isFly)
        {
            Vector3 lookPos = destinationPoint - plain.transform.position;
            if (lookPos != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                plain.transform.rotation = Quaternion.Slerp(plain.transform.rotation, rotation, Time.deltaTime * 10f);
            }
            if (Vector3.Distance(destinationPoint, transform.position) < 1f) StartCoroutine(Boom());
        }
    }

    IEnumerator Boom()
    {
        isFly = false;
        List<Rigidbody> rBodyes = new List<Rigidbody>();
        foreach (Collider col in Physics.OverlapSphere(transform.position, actionRadius))
        {
            if (col.tag == "GoodGuy" && col.transform.GetChild(0).GetComponent<SoldierController>().IsAlive)
            {
                Vector3 direction = transform.position - col.transform.position;
                Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                rb.velocity = Vector3.zero;
                rb.velocity = -direction.normalized*5f;
                rBodyes.Add(rb);
            }
        }
        yield return new WaitForSecondsRealtime(0.5f);
        foreach (Rigidbody rb in rBodyes)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.velocity = Vector3.zero;
        }
    }

}
