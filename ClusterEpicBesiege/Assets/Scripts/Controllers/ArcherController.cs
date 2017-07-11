using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ArcherController : MonoBehaviour, IMove, ISelectable, IDamageable
{

    public Animator animator;
    public GameObject arrow;
    public Transform arrowPoint;



    Vector3 point;

    GameObject indicator;
    GameObject target;

    NavMeshAgent agent;

    IMove realizationIMove;

    bool isAlive = true;
    bool isFind = false;
    bool isMoveToTarget = false;
    bool checkRotation = false;
    bool isMoving = false;

    float health = 100;
    float obstacleRange = 10f;
    float radiusAroundTarget = 5f;

    public bool Indicator { get { return indicator.activeSelf; } set { indicator.SetActive(value); } }
    public bool IsAlive { get { return isAlive; } }
    public Vector3 Positions { get { return transform.position; } }
    public IMove referenceIMove { get { return realizationIMove; } }

    private void Awake()
    {
        indicator = transform.GetChild(0).gameObject;
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        realizationIMove = gameObject.GetComponent<ArcherController>() as IMove;
        ISelectable realizationISelectable = gameObject.GetComponent<ArcherController>() as ISelectable;
        GameController.instance.AddFriendlUnit(gameObject, realizationIMove, realizationISelectable);
        point = new Vector3(UnityEngine.Random.Range(0.5f, 1.5f), transform.position.y, UnityEngine.Random.Range(0.5f, 1.5f));
        MoveTo(point);
        StartCoroutine(FindTarget());
    }

    private void Update()
    {
        if (isMoving && !isMoveToTarget)
        {
            if (Vector3.Distance(point, transform.position) < 0.5f)
            {
                isMoving = false;
                isFind = true;
                agent.Stop();
                agent.ResetPath();
            }
        }
        else if (isMoving && isMoveToTarget)
        {
            if (Vector3.Distance(point, transform.position) <= 5f)
            {
                isMoving = false;
                isMoveToTarget = false;
                agent.Stop();
                agent.ResetPath();
                checkRotation = true;
                StartCoroutine(AttackTarget(target));
            }
        }

        if (!isMoving && !isMoveToTarget && checkRotation && target != null)
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
        }

    }

    IEnumerator FindTarget()
    {
        for (;;)
        {
            if (isFind)
            {
                Collider[] colliders;
                float distance = 1000f;
                GameObject target = null;
                colliders = Physics.OverlapSphere(transform.position, obstacleRange);
                foreach (Collider col in colliders)
                {
                    if (col.tag == "badGuy")
                    {
                        var temp = Vector3.Distance(col.transform.position, transform.position);
                        if (distance > temp) { distance = temp; target = col.gameObject; }
                    }
                }
                if (target != null)
                {
                    isFind = false;
                    MoveTo(target.transform.position, target);
                }
            }
            yield return new WaitForSecondsRealtime(1f);
        }
    }

    public void DealDamage(float damage)
    {
        if (health > 0)
            health -= damage;
        else isAlive = false;
    }

    public void MoveTo(Vector3 position, GameObject target = null)
    {
        if (target != null)
        {
            isMoveToTarget = true;
            this.target = target;
            isMoveToTarget = true;
            point = (position - ((position - transform.position).normalized * 5f));
        }
        else
        {
            isMoveToTarget = false;
            point = position;
        }
        StopCoroutine(AttackTarget(target));
        animator.SetBool("isAttack", false);
        isMoving = true;
        isFind = false;
        checkRotation = false;
        agent.Resume();
        agent.SetDestination(point);
    }

    IEnumerator AttackTarget(GameObject target)
    {
        for (;;)
        {
            if (target != null)
            {
                if (target.GetComponent<IDamageable>().IsAlive && !isMoving)
                {
                    animator.SetBool("isAttack", true);
                }
                else break;
            }
            yield return new WaitForSecondsRealtime(2f);
        }
        animator.SetBool("isAttack", false);
        target = null;
        isFind = true;
    }

    public void Attack()
    {
        GameObject arrowprefab = Instantiate(arrow, arrowPoint.transform.position, arrowPoint.transform.rotation);
        if (target != null) StartCoroutine(MoveArrow(arrowprefab, target.transform.position));
    }

    IEnumerator MoveArrow(GameObject arrow, Vector3 targetPos)
    {
        for (;;)
        {
            if (target != null)
            {
                arrow.transform.position = Vector3.MoveTowards(arrow.transform.position, target.transform.position, 10f * Time.deltaTime);
                if (Vector3.Distance(arrow.transform.position, target.transform.position) < 0.3f)
                {
                    target.GetComponent<IDamageable>().DealDamage(20f);
                    break;
                }
            }
            else break;
            yield return new WaitForSecondsRealtime(0.02f);
        }
    }



}
