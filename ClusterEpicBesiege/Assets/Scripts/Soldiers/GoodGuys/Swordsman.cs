using UnityEngine;
public class Swordsman : MonoBehaviour, ISoldier
{
    // Swordsman stats
    float health = 130f;
    float attackRange = 1f;
    float attackPower = 25f;
    float detectionRadius = 7f;
    float movementSpeed = 2f;
    float rotationSpeed = 8f;

    public float AttackPower { get { return attackPower; } set { attackPower = value; } }
    public float AttackRange { get { return attackRange; } set { attackRange = value; } }
    public float DetectionRadius { get { return detectionRadius; } set { detectionRadius = value; } }
    public float Health { get { return health; } set { health = value; } }
    public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
    public float RotationSpeed { get { return rotationSpeed; } set { rotationSpeed = value; } }
    public SoldierType Type { get { return SoldierType.Swordsman; } }
    public TeamTag Tag { get { return TeamTag.GoodGuy; } }
}
