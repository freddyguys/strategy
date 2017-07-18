using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SoldierController : MonoBehaviour
{
    [SerializeField]
    private GameObject Soldier;
    [SerializeField]
    private GameObject Movement;
    [SerializeField]
    private GameObject Attack;
    [SerializeField]
    private GameObject Select;
    [SerializeField]
    private GameObject Damage;
    [SerializeField]
    private GameObject Weapon;


    public ISoldier SoldierInterface;
    public IAttackable AttackInterface;
    public IDamageable DamageInterface;
    public IMovable MoveInterface;
    public ISelectable SelectInterface;
    public IWeapon WeaponInterface;

    private Animator animator;
    private NavMeshAgent agent;
    private Transform myTransform;
    private TeamTag enemyTeamTag;

    private bool isAttack = false;
    private bool isAlive = true;
    private bool isFind = true;
    private bool isMove = false;
    private bool isMoveToTarget = false;
    public bool IsAttack { get { return isAttack; } set { isAttack = value; } }
    public bool IsAlive { get { return isAlive; } }
    public bool IsFind { get { return isFind; } set { isFind = value; } }
    public bool IsMove { get { return isMove; } set { isMove = value; } }
    public bool IsMoveToTarget { get { return isMoveToTarget; } set { isMoveToTarget = value; } }
    public TeamTag EnemyTeamTag { get { return enemyTeamTag; } set { enemyTeamTag = value; } }
    public Animator Animator { get { return animator; } }


    private SoldierController targetContrl;
    public SoldierController TargetContr { get { return targetContrl; } set { targetContrl = value; } }



    private void Awake()
    {
        animator = transform.parent.GetComponent<Animator>();
        SoldierInterface = Soldier.GetComponent<ISoldier>();
        AttackInterface = Attack.GetComponent<IAttackable>();
        MoveInterface = Movement.GetComponent<IMovable>();
        SelectInterface = Select.GetComponent<ISelectable>();
        DamageInterface = Damage.GetComponent<IDamageable>();
        WeaponInterface = Weapon.GetComponent<IWeapon>();
        agent = transform.parent.GetComponent<NavMeshAgent>();
        myTransform = transform.parent.GetComponent<Transform>();
        enemyTeamTag = SoldierInterface.Tag == TeamTag.BadGuy ? TeamTag.GoodGuy : TeamTag.BadGuy;
    }

    private void Start()
    {
        GameController.instance.AddlSoldier(GetComponent<SoldierController>());
    }

    private void Update()
    {
        if (isFind)
        {
            float oldDistance = 1000f;
            SoldierController _targetContrl = null;
            foreach (Collider col in Physics.OverlapSphere(transform.position, SoldierInterface.DetectionRadius))
            {
                if (col.tag == enemyTeamTag.ToString() && col.transform.GetChild(0).GetComponent<SoldierController>().IsAlive)
                {
                    var currentDistance = Vector3.Distance(col.transform.position, GetPositions());
                    if (oldDistance > currentDistance) { oldDistance = currentDistance; _targetContrl = col.transform.GetChild(0).GetComponent<SoldierController>(); }
                }
            }
            if (_targetContrl != null)
            {
                isFind = false;
                targetContrl = _targetContrl;
                AttackInterface.AttackTarget(targetContrl);
            }
        }

        if (isMove && isMoveToTarget && targetContrl != null)
        {
            if (Vector3.Distance(GetPositions(), TargetContr.GetPositions()) <= SoldierInterface.AttackRange)
            {
                isMove = false;
                isMoveToTarget = false;
                agent.Stop();
                agent.ResetPath();
                AttackInterface.AttackTarget(targetContrl);
            }
            else MoveInterface.MoveTo(TargetContr.GetPositions());

        }

        if (isMove && !isMoveToTarget)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                isMove = false;
                isFind = true;
            }
        }

        if (targetContrl != null)
        {
            Vector3 lookPos = targetContrl.GetPositions() - GetPositions();
            if (lookPos != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                myTransform.rotation = Quaternion.Slerp(myTransform.rotation, rotation, Time.deltaTime * SoldierInterface.RotationSpeed);
            }
        }
    }

    public Vector3 GetPositions()
    { return transform.position; }

    public void SetTeam(TeamTag tag)
    {
        SoldierInterface.Tag = tag;
        transform.parent.gameObject.tag = tag.ToString();
        SelectInterface.ChangeColor(SoldierInterface.Tag);
        targetContrl = null;
        enemyTeamTag = SoldierInterface.Tag == TeamTag.BadGuy ? TeamTag.GoodGuy : TeamTag.BadGuy;
    }

    public void StopSoldier()
    {
        agent.Stop();
        agent.ResetPath();
    }

    public void Death()
    {
        isAlive = false;
        GameController.instance.DeleateSoldier(GetComponent<SoldierController>());
        Destroy(transform.parent.gameObject);
    }
}
