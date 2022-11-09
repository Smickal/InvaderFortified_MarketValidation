using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAttributes : MonoBehaviour
{
    [Header("Upgrade Details")]
    [SerializeField] public int damageUpgrade = 1;
    [SerializeField] public int rangeUpgrade = 1;
    [SerializeField] int maxHPFactoryUpgrade = 20;

    [Header("Upgrade Price")]
    [SerializeField] float damageUpgradePrice = 10;
    [SerializeField] float rangeUpgradePrice = 10;
    [SerializeField] float recoverFactoryPrice = 10;
    [SerializeField] float maxFactoryUpgradePrice = 10;

    [Header("MaxUpgradeCounter")]
    [SerializeField] float damageCounter = 3;
    [SerializeField] float rangeCounter = 3;
    [SerializeField] float maxHPCounter = 3;

    float currentDamageCounter = 0;
    float currentRangeCounter = 0;
    float currentMaxHPCounter = 0;

    [SerializeField] private SpawnMinions spawnMinions;
    UpgradePanel upgradePanel;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        
    }
    
    public void UpgradeDamage()
    {
        //edit all current minion damage
        //set the new damage as standard damage
        upgradePanel = FindObjectOfType<UpgradePanel>();
        if (gameManager.Currency >= damageUpgradePrice && currentDamageCounter != damageCounter)
        {
            Turrets[] currentTurrets = GetComponentsInChildren<Turrets>();
            float tempNewDamage = spawnMinions.turretDamage + damageUpgrade;
            foreach (Turrets turret in currentTurrets)
            {
                turret.turretDamage = tempNewDamage;
            }

            spawnMinions.turretDamage = tempNewDamage;

            gameManager.Currency -= damageUpgradePrice;
            currentDamageCounter++;
            if (currentDamageCounter == damageCounter)
                upgradePanel.CheckDamageText(false);
        }
    }

    public void UpgradeRange()
    {
        upgradePanel = FindObjectOfType<UpgradePanel>();
        if (gameManager.Currency >= rangeUpgradePrice)
        {
            Turrets[] currentTurrets = GetComponentsInChildren<Turrets>();
            float tempNewRange = spawnMinions.turretRange + rangeUpgrade;
            foreach(Turrets turret in currentTurrets)
            {
                turret.turretRange = tempNewRange;
            }
            spawnMinions.turretRange = tempNewRange;

            gameManager.Currency -= rangeUpgradePrice;
            currentRangeCounter++;
            if (currentRangeCounter == rangeCounter)
                upgradePanel.CheckRangeText(false);
        }
    }

    public void HealFactory()
    {
        if(gameManager.Currency >= recoverFactoryPrice && spawnMinions.factoryHP < spawnMinions.maxFactoryHP)
        {
            spawnMinions.factoryHP = spawnMinions.maxFactoryHP;
            gameManager.Currency -= recoverFactoryPrice;
        }
    }

    //public void IncreaseMaxFactoryHP()
    //{
    //    if (gameManager.Currency >= maxFactoryUpgradePrice)
    //    {
    //        float tempMaxFactoryHP = spawnMinions.maxFactoryHP + maxHPFactoryUpgrade;
    //        spawnMinions.maxFactoryHP = tempMaxFactoryHP;

    //        gameManager.Currency -= maxFactoryUpgradePrice;
    //        currentMaxHPCounter++;
    //        if (currentMaxHPCounter == maxHPCounter)
    //            upgradePanel.CheckMaxHpText(false);
    //    }
    //}

    public bool CheckDamageCounter()
    {
       return (currentDamageCounter >= damageCounter ?  false :  true);
    }

    public bool CheckRangeCounter()
    {
        return (currentRangeCounter >= rangeCounter ? false : true);
    }

    public bool CheckMaxHPCounter()
    {
        return (currentMaxHPCounter >= maxHPCounter ? false : true); 
    }
}
