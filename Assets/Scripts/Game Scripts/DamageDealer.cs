using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    float damageDeals = 10f;
    

    public void GetDamage(float turretDamage)
    {
        damageDeals = turretDamage;
    }
}
