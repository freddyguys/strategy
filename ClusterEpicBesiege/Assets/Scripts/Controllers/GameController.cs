using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private List<SoldierController> soldiers = new List<SoldierController>();

    public static GameController instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void AddlSoldier(SoldierController soldier)
    {
        soldiers.Add(soldier);
    }

    public void DeleateSoldier(SoldierController soldier)
    {
        foreach (SoldierController sc in soldiers.ToArray())
        {
            if (soldier == sc) soldiers.Remove(sc);
        }
    }

    public List<SoldierController> GetSoldiers()
    {
        return soldiers;
    }

    void Start()
    {
        IsSpawn = true;
        IsSpawnGoodGuy = true;
    }

    public bool IsSpawn { get { return SoldierSpawner.instance.isSpawn; } set { SoldierSpawner.instance.isSpawn = value; } }
    public bool IsSpawnGoodGuy { get { return SoldierSpawner.instance.isSpawnGoodGuy; } set { SoldierSpawner.instance.isSpawnGoodGuy = value; } }
    public bool IsSpawnBadGuy { get { return SoldierSpawner.instance.isSpawnBadGuy; } set { SoldierSpawner.instance.isSpawnBadGuy = value; } }
}
