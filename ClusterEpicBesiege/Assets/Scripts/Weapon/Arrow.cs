using UnityEngine;

public class Arrow : MonoBehaviour
{
    Transform plain;
    bool oneShot = false;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == enemyTeamTag.ToString() && !oneShot)
        {
            oneShot = true;
            collision.transform.GetChild(0).GetComponent<SoldierController>().DamageInterface.DealDamage(attackPower);
            Destroy(gameObject, 1.5f);
        }
        else Destroy(gameObject, 5f);
    }

    private void Update()
    {
        Vector3 lookPos = destinationPoint - plain.transform.position;
        if (lookPos != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            plain.transform.rotation = Quaternion.Slerp(plain.transform.rotation, rotation, Time.deltaTime * 10f);
        }
    }
}
