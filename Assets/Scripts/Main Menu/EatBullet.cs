using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
