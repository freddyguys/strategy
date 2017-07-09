using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SoldierController : MonoBehaviour, IMove, ISelectable, IDamageable
{
    public Soldier soldier;

    public Animator animator;

    public Transform swordTransform;

    private GameObject indicator;
    private float speed = 1f;
    private float obstacleRange = 5f;
    private float health = 100;
    private float attackDistance = 0.4f;

    private Vector3 point;

    private Vector3 attackPosition;

    private bool isMoveToTarget = false;
    private bool isTouch = false;
    private bool isFind = false;
    private bool isMoving = false;
    private bool checkRange = false;
    private bool isAlive = true;

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
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || Mathf.Abs(agent.velocity.sqrMagnitude) < float.Epsilon)
                {
                    isMoving = false;
                    isFind = true;
                    agent.Stop();
                }
            }
        }
        else if (isMoving && isMoveToTarget)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || Mathf.Abs(agent.velocity.sqrMagnitude) < float.Epsilon)
                {
                    isMoving = false;
                    isMoveToTarget = false;
                    agent.Stop();
                    //StartCoroutine(AttackTarget(target));
                }
            }
        }
        //_direction = (tempPos - transform.position).normalized;
        //_lookRotation = Quaternion.LookRotation(_direction);
        //transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 100f);
    }

    public void MoveTo(Vector3 position, GameObject target = null)
    {
        if (target != null)
        {
            isMoveToTarget = true;
            this.target = target;
            Vector3 offset = new Vector3(Mathf.Sin(UnityEngine.Random.Range(-360f, 360f)), 0f, Mathf.Cos(UnityEngine.Random.Range(-360f, 360f))) * 0.4f;
            //point = (position - ((position - transform.position).normalized * 0.5f))+offset;
            point = position + offset;
            print(point);
        }
        else
        {
            isMoveToTarget = false;
            point = position - ((position - transform.position).normalized);
        }
        isMoving = true;
        isFind = false;
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
                colliders = Physics.OverlapSphere(transform.position, obstacleRange);
                foreach (Collider col in colliders)
                {
                    if (col.tag == "badGuy")
                    {
                        MoveTo(col.transform.position, col.gameObject);
                    }
                }
            }
            yield return new WaitForSecondsRealtime(1f);
        }
    }


    IEnumerator AttackTarget(GameObject target)
    {
        bool isAliveTarget = target.GetComponent<IDamageable>().IsAlive;
        for (;;)
        {
            if (isAliveTarget)
            {
                animator.SetTrigger("attack");
            }
            else break;
            yield return new WaitForSecondsRealtime(1f);
        }
        target = null;
    }


    //   private void Attack()
    //  {

    // RaycastHit hit;
    // var up = swordTransform.TransformDirection(Vector3.right);
    //Debug.DrawRay(swordTransform.position, up, Color.green);

    // if (Physics.Raycast(swordTransform.position, up, out hit))
    // {
    //  if (hit.collider.tag == "badGuy")
    //   {
    //     isTouch = true;
    //hit.collider.GetComponent<IDamageable>().dealDamage(12f);
    //Invoke("DontTouch", 0.5f);
    //  }
    // }
    // }

    //  private void DontTouch()
    //  {
    //      isTouch = false;
    //  }

    //private void CheckAttackRange(Vector3 targetPotition)
    //{
    //    checkRange = true;
    //    //attackPosition = targetPotition - ((targetPotition - transform.position).normalized * 0.5f);
    //}

    public void DealDamage(float damage)
    {
        if (health > 0)
            health -= damage;
        else isAlive = false;
    }
}
