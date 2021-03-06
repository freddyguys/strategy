﻿using UnityEngine;
public class Archer : MonoBehaviour, ISoldier
{
    // Archer stats
    float health = 100f;
    float attackRange = 9f;
    float attackPower = 10f;
    float detectionRadius = 10f;
    float movementSpeed = 3f;
    float rotationSpeed = 10f;
    float cooldown = 2f;
    TeamTag tag = TeamTag.GoodGuy;

    public float AttackPower { get { return attackPower; } set { attackPower = value; } }
    public float AttackRange { get { return attackRange; } set { attackRange = value; } }
    public float DetectionRadius { get { return detectionRadius; } set { detectionRadius = value; } }
    public float Health { get { return health; } set { health = value; } }
    public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
    public float RotationSpeed { get { return rotationSpeed; } set { rotationSpeed = value; } }
    public float Cooldown { get { return cooldown; } set { cooldown = value; } }
    public SoldierType Type { get { return SoldierType.Archer; } }
    public TeamTag Tag { get { return tag; } set { tag = value; } }
}
