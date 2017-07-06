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
    private float obstacleRange = 2f;
    private float health = 100;

    private Vector3 point;
    private Vector3 attackRange = new Vector3(0.5f, 0f, 0.5f);
    private Vector3 attackPosition;

    private bool isTouch = false;
    private bool isFind = true;
    private bool isMoving = false;
    private bool canAttack = false;

    NavMeshAgent agent;

    private IMove realizationIMove;


    public Vector3 Positions { get { return transform.position; } }

    public bool Indicator { get { return indicator.activeSelf; } set { indicator.SetActive(value); } }

    public IMove referenceIMove { get { return realizationIMove; } }

    void Start()
    {
        indicator = transform.GetChild(0).gameObject;
        realizationIMove = gameObject.GetComponent<SoldierController>() as IMove;
        ISelectable realizationISelectable = gameObject.GetComponent<SoldierController>() as ISelectable;
        GameController.instance.AddFriendlUnit(gameObject, realizationIMove, realizationISelectable);
        point = new Vector3(UnityEngine.Random.Range(0.5f, 1.5f), transform.position.y, UnityEngine.Random.Range(0.5f, 1.5f));
        StartCoroutine(FindTarget());
        MoveTo(point);
        agent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        if (isMoving)
        {
            //transform.position = Vector3.MoveTowards(transform.position, point, speed * Time.deltaTime);
            //Quaternion rotation = Quaternion.LookRotation(transform.position - point);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed * 3f);
            agent.SetDestination(point);
            if (agent.remainingDistance <= float.Epsilon)
            { isMoving = false; isFind = true; }

            //if (transform.position == point) { isMoving = false; isFind = true; }


        }

        if (canAttack)
        {
            if (transform.position == attackPosition) { Attack(); canAttack = false; }
        }
    }

    public void MoveTo(Vector3 position)
    {
        isMoving = true;
        isFind = false;
        canAttack = false;
        point = position - ((position - transform.position).normalized * 0.5f);
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
                        MoveTo(col.transform.position);
                        CheckAttackRange(col.transform.position);
                    }
                }
            }
            yield return new WaitForSecondsRealtime(1.5f);
        }
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
        RaycastHit hit;
        var up = swordTransform.TransformDirection(Vector3.right);
        //Debug.DrawRay(swordTransform.position, up, Color.green);

        if (Physics.Raycast(swordTransform.position, up, out hit))
        {
            if (hit.collider.tag == "badGuy" && !isTouch)
            {
                isTouch = true;
                hit.collider.GetComponent<IDamageable>().dealDamage(12f);
                Invoke("DontTouch", 1.2f);
            }
        }
    }

    private void DontTouch()
    {
        isTouch = false;
    }

    private void CheckAttackRange(Vector3 targetPotition)
    {
        canAttack = true;
        attackPosition = targetPotition - ((targetPotition - transform.position).normalized * 0.5f);
    }

    public void dealDamage(float damage)
    {
        if (health > 0)
            health -= damage;
        else Destroy(gameObject);
    }
}
