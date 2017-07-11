using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private List<ISelectable> realizationISelectable = new List<ISelectable>();
    private List<IMove> realizationIMove = new List<IMove>();
    private List<GameObject> unit;

    public static GameController instance;

    private void Awake()
    {
        unit = new List<GameObject>();
        if (instance == null) instance = this;
    }

    public void AddFriendlUnit(GameObject unitObj, IMove iMove, ISelectable iSelectable)
    {
        unit.Add(unitObj);
        realizationIMove.Add(iMove);
        realizationISelectable.Add(iSelectable);
    }

    public List<IMove> GetIMoveRealization()
    {
        return realizationIMove;
    }

    public List<ISelectable> GetISelectableRealization()
    {
        return realizationISelectable;
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
