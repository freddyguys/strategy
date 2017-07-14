using System.Collections;
using UnityEngine;

public class SoldierSpawner : MonoBehaviour
{

    public static SoldierSpawner instance;

    public GameObject spawnPointForGoodGuys;
    private Vector3 goodPoint;

    public bool isSpawn;
    public bool isSpawnGoodGuy;
    public bool isSpawnBadGuy;

    public float delay = 5f;
    private float remainigTime;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(Spawn());
        goodPoint = new Vector3(spawnPointForGoodGuys.transform.position.x + 0.36f, spawnPointForGoodGuys.transform.position.y, spawnPointForGoodGuys.transform.position.z);
    }

    IEnumerator Spawn()
    {
        for (;;)
        {
            yield return new WaitForSecondsRealtime(delay);
            if (isSpawn)
            {
                if (isSpawnGoodGuy) AddGoodGuy();
                if (isSpawnBadGuy) AddBadGuy();
            }
        }
    }

    private void AddGoodGuy()
    {
        GameObject soldier = Instantiate(SpawnController.instance.GetCurrenSoldier(), goodPoint, transform.rotation);
    }

    private void AddBadGuy()
    {

    }
}
