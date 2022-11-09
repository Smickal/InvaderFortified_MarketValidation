using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuEnemies : MonoBehaviour
{
    public float enemySpeed = 10f;
    private Transform target;
    private int wavepointIndex = 0;
    public float enemyHealth = 0;

    private void Start()
    {
        target = Waypoints.waypoints[1];
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 direction = target.position - transform.localPosition;
        transform.Translate(direction.normalized * enemySpeed * Time.deltaTime, Space.World);
        if(Vector2.Distance(transform.position, target.position) <= 0.2f)
        {
            if(wavepointIndex >= Waypoints.waypoints.Length -1)
            {
                Destroy(gameObject);
                return;
            }
            wavepointIndex++;
            target = Waypoints.waypoints[wavepointIndex];
        }
    }
}
