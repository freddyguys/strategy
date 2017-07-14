using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour, IMovable
{
    private NavMeshAgent agent;
    private SoldierController myContrl;

    private void Awake()
    {
        agent = transform.parent.transform.parent.GetComponent<NavMeshAgent>();
        myContrl = transform.parent.GetComponent<SoldierController>();
    }

    public void MoveTo(Vector3 position)
    {
        myContrl.IsFind = false;
        myContrl.IsMove = true;
        agent.Resume();
        agent.speed = myContrl.SoldierInterface.MovementSpeed;
        agent.SetDestination(position);
    }
}
