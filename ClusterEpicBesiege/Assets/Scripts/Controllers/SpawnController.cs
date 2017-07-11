using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject[] goodGuys;

    public static SpawnController instance;

    private GameObject currentSoldier;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentSoldier = goodGuys[0];
    }

    public GameObject GetCurrenSoldier()
    {
        return currentSoldier;
    }

    public void SwordsmanSpawn()
    {
        currentSoldier = goodGuys[0];
    }
    public void ArcherSpawn()
    {
        currentSoldier = goodGuys[1];
    }
}
