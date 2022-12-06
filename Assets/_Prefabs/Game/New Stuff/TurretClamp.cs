using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretClamp : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("TopClamp")]
    [SerializeField] float maxClamp;
    [SerializeField] float minClamp;
    int idx = 0;

    // Update is called once per frame
    public void ClampTurret(Turrets turret)
    {
        //set Clamps
        if(idx != 0)
        {
            float temp = minClamp;
            minClamp = -maxClamp;
            maxClamp = temp;
        }
        //turret.SetClamp(minClamp, maxClamp);
        idx++;
    }

}
