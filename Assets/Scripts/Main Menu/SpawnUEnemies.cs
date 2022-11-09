using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUEnemies : MonoBehaviour
{
    [SerializeField] GameObject Enemies;
    

    [SerializeField] float delayBetweenSpawns = 2f;
    private Transform target;
    float tempDelay;
    bool isSpawned = false;
    float currDelay;
    void Start()
    {
        target = Waypoints.waypoints[0];
        tempDelay = delayBetweenSpawns;
        currDelay = delayBetweenSpawns;
    }

    private void Update()
    {
        if(!isSpawned)
        { 
            SpawnEnemies();
        }
        currDelay = UnityEngine.Random.Range(0.1f, delayBetweenSpawns);
    }

    void SpawnEnemies()
    {
        StartCoroutine(SpawnDelayedEnemies());
    }

    IEnumerator SpawnDelayedEnemies()
    {
        isSpawned = true;
        for (int i = 0; i < 10; i++)
        {
            GameObject newEnemies = Instantiate(Enemies, target.transform.position, transform.localRotation);
            yield return new WaitForSeconds(currDelay); 
        }
        isSpawned = false;

    }

}
