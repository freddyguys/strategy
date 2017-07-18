using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airman : MonoBehaviour, ISoldier
{
    // Airman stats
    float health = 80f;
    float attackRange = 6f;
    float attackPower = 30f;
    float detectionRadius = 10f;
    float movementSpeed = 4f;
    float rotationSpeed = 10f;
    float cooldown = 3f;
    TeamTag tag = TeamTag.BadGuy;

    public float AttackPower { get { return attackPower; } set { attackPower = value; } }
    public float AttackRange { get { return attackRange; } set { attackRange = value; } }
    public float DetectionRadius { get { return detectionRadius; } set { detectionRadius = value; } }
    public float Health { get { return health; } set { health = value; } }
    public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
    public float RotationSpeed { get { return rotationSpeed; } set { rotationSpeed = value; } }
    public float Cooldown { get { return cooldown; } set { cooldown = value; } }
    public SoldierType Type { get { return SoldierType.Airman; } }
    public TeamTag Tag { get { return tag; } set { tag = value; } }

}
