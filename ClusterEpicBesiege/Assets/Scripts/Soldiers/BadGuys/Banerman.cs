using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banerman : MonoBehaviour, ISoldier
{
    float health = 200f;
    float attackRange = 0.7f;
    float attackPower = 40f;
    float detectionRadius = 6f;
    float movementSpeed = 2f;
    float rotationSpeed = 5f;

    public float AttackPower { get { return attackPower; } set { attackPower = value; } }
    public float AttackRange { get { return attackRange; } set { attackRange = value; } }
    public float DetectionRadius { get { return detectionRadius; } set { detectionRadius = value; } }
    public float Health { get { return health; } set { health = value; } }
    public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
    public float RotationSpeed { get { return rotationSpeed; } set { rotationSpeed = value; } }
    public SoldierType Type { get { return SoldierType.Banerman; } }
    public TeamTag Tag { get { return TeamTag.BadGuy; } }
}
