using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SoldierController : MonoBehaviour, IMove, ISelectable, IDamageable
{
    public Soldier soldier;
    public Transform weapon;
    public Animator animator;


    private GameObject indicator;
    private float speed = 1f;
    private float obstacleRange = 5f;
    private float health = 100;
    private float attackDistance = 0.4f;

    private Vector3 point;

    private Vector3 attackPosition;

    private bool isMoveToTarget = false;
    private bool isFind = false;
    private bool isMoving = false;
    private bool checkRange = false;
    private bool isAlive = true;
    private bool checkRotation = false;

    private GameObject target;

    NavMeshAgent agent;

    private IMove realizationIMove;
    public bool IsAlive { get { return isAlive; } }
    public Vector3 Positions { get { return transform.position; } }
    public bool Indicator { get { return indicator.activeSelf; } set { indicator.SetActive(value); } }
    public IMove referenceIMove { get { return realizationIMove; } }

    private void Awake()
    {
        indicator = transform.GetChild(0).gameObject;
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        realizationIMove = gameObject.GetComponent<SoldierController>() as IMove;
        ISelectable realizationISelectable = gameObject.GetComponent<SoldierController>() as ISelectable;
        GameController.instance.AddFriendlUnit(gameObject, realizationIMove, realizationISelectable);
        // first position after spawn
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
                animator.SetBool("move", false);
                isMoving = false;
                isFind = true;
                agent.Stop();
                agent.ResetPath();
            }
        }
        else if (isMoving && isMoveToTarget)
        {
            if (Vector3.Distance(point, transform.position) < 1f)
            {
                animator.SetBool("move", false);
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

    public void MoveTo(Vector3 position, GameObject target = null)
    {
        if (target != null)
        {
            isMoveToTarget = true;
            this.target = target;
            Vector3 offset = new Vector3(Mathf.Sin(UnityEngine.Random.Range(-360f, 360f)), 0f, Mathf.Cos(UnityEngine.Random.Range(-360f, 360f))) * 0.8f;
            point = position + offset;
        }
        else
        {
            isMoveToTarget = false;
            //point = (position - ((position - transform.position).normalized * 1f));
            point = position;
        }
        animator.SetBool("attack", false);
        animator.SetBool("move", true);
        StopCoroutine(AttackTarget(target));
        isMoving = true;
        isFind = false;
        checkRotation = false;
        agent.Resume();
        agent.SetDestination(point);
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

    IEnumerator AttackTarget(GameObject target)
    {
        for (;;)
        {
            if (target != null)
            {
                if (target.GetComponent<IDamageable>().IsAlive && !isMoving)
                {
                    animator.SetBool("attack", true);
                }
                else break;
            }
            yield return new WaitForSecondsRealtime(1f);
        }
        target = null;
        animator.SetBool("attack", false);
        isFind = true;
    }

    public void DealDamage(float damage)
    {
        if (health > 0)
            health -= damage;
        else isAlive = false;
    }

    public void Attack()
    {
        RaycastHit hit;
        if (target != null && Physics.Raycast(transform.position, target.transform.position - transform.position, out hit, 1.5f))
        {
            if (hit.collider.tag == "badGuy") hit.collider.GetComponent<IDamageable>().DealDamage(10f);
        }
    }
}
