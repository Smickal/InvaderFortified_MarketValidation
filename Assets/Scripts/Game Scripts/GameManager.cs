using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{

    private float currency = 0;
    [Header("Localization")]
    [SerializeField]private TextMeshProUGUI currencyText;
    [SerializeField]private TextMeshProUGUI playerHpText;
    [SerializeField]private GameObject gameOverButton;

    ExtraMainTowerAttributes extraMainTowerAttributes;

    DragAndDrop dragAndDrop;
    UpgradePanel upgradePanel;
    public float Currency
    {
        get
        {
            return currency;
        }

        set
        {
            this.currency = value;
            this.currencyText.text = value.ToString() + " $";
        }
    }

    private void Start() 
    {
        Currency = 2000;

        upgradePanel.DisableUpgradePanel();
    }
    private void Awake() 
    {
        upgradePanel = FindObjectOfType<UpgradePanel>();
        int numOfGameManagerInstance = FindObjectsOfType<GameManager>().Length;
        dragAndDrop = FindObjectOfType<DragAndDrop>();
        extraMainTowerAttributes = FindObjectOfType<ExtraMainTowerAttributes>();
        if(numOfGameManagerInstance > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update() 
    {
        DisplayMainTowerHP();
        CheckForGameOverScreen();
    }

    private void CheckForGameOverScreen()
    {
        if(extraMainTowerAttributes.MainTowerHp <= 0)
        {
            gameOverButton.SetActive(true);
        }
        else
        {
            gameOverButton.SetActive(false);
        }
    }

    public Shop ClickedBtn { get;  set; }
    
    public void PickTower(Shop towerBtn)
    {
        if(this.ClickedBtn != null)
        {
            if(this.ClickedBtn == towerBtn)
            {
                CancelCurrentTower();
                return;
            }
        }
        

        this.ClickedBtn = towerBtn;

        if(Currency >= towerBtn.GetTowerCost())
        {
            //Debug.Log(towerBtn.tag);
            if(towerBtn.tag == "CurrencyFactory")
            {
                dragAndDrop.Activate(towerBtn.GetSpriteOfTower());
            }
            else
            {
                dragAndDrop.SetRangeViewer(towerBtn.GetTowerRange());   
                dragAndDrop.Activate(towerBtn.GetSpriteOfTower());

                RefreshUpgradePanel();
            }

        }
        else
        {
            this.ClickedBtn = null;
        }

    }

    void RefreshUpgradePanel()
    {
        SpawnMinions factorySelected = ClickedBtn.GetComponent<Shop>().GetTowerPrefab().GetComponentInChildren<SpawnMinions>();
        UpgradeAttributes upgradeAttributes = factorySelected.GetComponent<UpgradeAttributes>();

        upgradePanel.SetTowerAttributes(factorySelected.turretName, factorySelected.factoryHP, factorySelected.maxFactoryHP,factorySelected.turretDamage, factorySelected.turretRange,
            factorySelected.fireRate, factorySelected.delayBetweenSpawns);
        upgradePanel.SetUpgradeAttributes(upgradeAttributes.damageUpgrade, upgradeAttributes.rangeUpgrade);

        upgradePanel.EnableUpgradePanelWhenButtoneClicked();

    }
    
    public void BuyTower()
    {
        Currency -= ClickedBtn.GetTowerCost();
        ClickedBtn = null;
        dragAndDrop.Deactivate();

    }

    public void CancelCurrentTower()
    {
        if(ClickedBtn != null)
        {
            ClickedBtn = null;
            dragAndDrop.Deactivate();
            upgradePanel.DisableUpgradePanel();
            Debug.Log("Cancelled");
        }
    }



    public void AddCurrency(float addCurrency)
    {
        Currency += addCurrency;
    }

    private void DisplayMainTowerHP()
    {
        playerHpText.text =  "HP: " + extraMainTowerAttributes.MainTowerHp.ToString();
    }

    
}
