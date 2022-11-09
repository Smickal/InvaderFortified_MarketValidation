using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    
    public static BuildManager instance;

    private void Awake() 
    {
        if(instance != null)
        {
            Debug.Log("More than 1 buildManager in screen");
            return;
        }
        instance = this;
    }

    public GameObject standardTurretPrefab;

    private void Start() 
    {
        turretToBuild = standardTurretPrefab;
    }

    private GameObject turretToBuild;

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    
}
