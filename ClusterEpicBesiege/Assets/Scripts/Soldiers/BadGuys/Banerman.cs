using UnityEngine;

public class Banerman : MonoBehaviour, ISoldier
{
    float health = 200f;
    float attackRange = 1.2f;
    float attackPower = 0f;
    float detectionRadius = 6f;
    float movementSpeed = 2f;
    float rotationSpeed = 5f;
    float cooldown = 2f;
    TeamTag tag = TeamTag.BadGuy;

    public float AttackPower { get { return attackPower; } set { attackPower = value; } }
    public float AttackRange { get { return attackRange; } set { attackRange = value; } }
    public float DetectionRadius { get { return detectionRadius; } set { detectionRadius = value; } }
    public float Health { get { return health; } set { health = value; } }
    public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
    public float RotationSpeed { get { return rotationSpeed; } set { rotationSpeed = value; } }
    public float Cooldown { get { return cooldown; } set { cooldown = value; } }
    public SoldierType Type { get { return SoldierType.Banerman; } }
    public TeamTag Tag { get { return tag; } set { tag = value; } }
}
