using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpawnMinions : MonoBehaviour
{
    [SerializeField] GameObject turret;
    [SerializeField] PreviewRangeOfFactory previewRangeOfFactory;

    public float delayBetweenSpawns = 2f;
    Transform[] spawnTurretPoints;
    float secondCountdown = 0f;
    int currentTowerCount = 0;

    [Header("Factory Attributes")]
    public float factoryHP = 100;
    public float maxFactoryHP;
    public float factoryCost;
    public int maxMinion = 2;

    [Header("Minion Attributes")]
    public string turretName;
    public float turretHP = 10f;
    public float turretRange = 15f;
    public float turretTurnSpeed = 10f;
    public float fireRate = 1f;

    [Header("Minions")]
    public float turretDamage = 10f;
    public float bulletSpeed = 5f;

    [Header("HP Bar")]
    [SerializeField] private Slider hpSlider;

    Turrets turretsToBeSpawned;
    private void Awake()
    {
        spawnTurretPoints = new Transform[transform.childCount];
        for (int i = 0; i < spawnTurretPoints.Length; i++)
        {
            spawnTurretPoints[i] = transform.GetChild(i);
        }
        secondCountdown = delayBetweenSpawns;

        turretsToBeSpawned = turret.GetComponent<Turrets>();
        GetTurretAttributes();
        maxFactoryHP = factoryHP;
        previewRangeOfFactory.SetRangeViewer(turretRange);
    }

    private void Update()
    {
        if (currentTowerCount < spawnTurretPoints.Length && secondCountdown <= 0)
        {
            BuildMinnion(currentTowerCount);
            secondCountdown = delayBetweenSpawns;
        }
        secondCountdown -= Time.deltaTime;

    
        if(currentTowerCount < maxMinion)
        {
            BuildMinnion(currentTowerCount);
        }

    }

    void BuildMinnion(int placeToSpawn)
    {
        GameObject newMinion =  Instantiate(turret, spawnTurretPoints[placeToSpawn].position, 
            Quaternion.identity, spawnTurretPoints[placeToSpawn].transform ) as GameObject;
        TurretClamp tc = GetComponent<TurretClamp>();
        if (tc)
            tc.ClampTurret(newMinion.GetComponent<Turrets>());
        secondCountdown = delayBetweenSpawns;
        currentTowerCount++;

    }

    void GetTurretAttributes()
    {
        this.turretHP = turretsToBeSpawned.turretHP;
        this.turretRange = turretsToBeSpawned.turretRange;
        this.fireRate = turretsToBeSpawned.fireRate;
        this.turretDamage = turretsToBeSpawned.turretDamage;
        this.turretName = turretsToBeSpawned.turretName;
    }
    
    public float GetCurrentFactoryHP()
    {
        return factoryHP;
    }
    
    void HealthBarDisplay()
    {
        hpSlider.value = factoryHP;
        hpSlider.maxValue = maxFactoryHP;
    }

    public void TakeDamage(float damage)
    {
        factoryHP -= damage;
        if(factoryHP <= 0)
        {
            Node node = GetComponentInParent<Node>();
            node.DestroyCurrentFactory();
        }
    }

    public void ReduceCurrentTower()
    {
        currentTowerCount--;
    }    
}
