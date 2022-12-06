using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    private Color startColor;


    SpriteRenderer colorRenderer;
    GameManager gameManager;
    UpgradePanel upgradePanel;

    SpawnMinions spawnMinions;
    allNodes parentNodes;

    [SerializeField] private GameObject turret;

    [Header("Upgrade Buttons")]
    [SerializeField] Button damageIncreaseButton;
    [SerializeField] Button rangeIncreaseButton;
    [SerializeField] Button recoverHealthButton;
    [SerializeField] Button increaseMaxFactoryUpgradeButton;
    [SerializeField] Button cancelButton;
    [SerializeField] Button deleteFactoryButton;


    public bool isHoverActivated = false;
    bool isFactoryPlaced = false;
    [SerializeField] bool isCurrentNodeSelected = false;
    [SerializeField] private bool isThisNodeSpecial = false;

    private Color32 fullColor = new Color32(255, 118, 118, 255);
    private Color32 emptyColor = new Color32(96, 255, 90, 255);


    bool isCalledButton = false;
    private void Awake()
    {
        colorRenderer = GetComponentInChildren<SpriteRenderer>();
        startColor = colorRenderer.color;
        gameManager = FindObjectOfType<GameManager>();
        upgradePanel = FindObjectOfType<UpgradePanel>();
        parentNodes = FindObjectOfType<allNodes>();

        if (!isThisNodeSpecial)
        {
            damageIncreaseButton.onClick.AddListener(UpgradeDamage);
            rangeIncreaseButton.onClick.AddListener(UpgradeRange);
           // recoverHealthButton.onClick.AddListener(HealCurrentFactory);
            //increaseMaxFactoryUpgradeButton.onClick.AddListener(UpgradeMaxHP);
            cancelButton.onClick.AddListener(parentNodes.DisableAllPreviews);
        }
        deleteFactoryButton.onClick.AddListener(DeleteCurrentFactory);
    }


    public void DisableCurrentSelectedNode()
    {
        isCurrentNodeSelected = false;
    }

    private void OnMouseDown()
    {
        PlaceTower();

        if (gameManager.ClickedBtn == null && turret != null && isFactoryPlaced == true && turret.tag != "CurrencyFactory")
        {
            RefreshUpdatePanel();
            upgradePanel.EnableUpgradePanel();
            //Check CurrentUpgradeCounter
            CheckCurrentUpgradeCounter();
        }
        
        if(!isFactoryPlaced)
        {
            Debug.Log("disabled Panel");
            upgradePanel.DisableUpgradePanel();
            parentNodes.DisableAllPreviews();
        }

    }

    private void OnMouseEnter()
    {
        if (gameManager.ClickedBtn != null)
        {
            if (gameManager.ClickedBtn.GetTowerPrefab().tag == "Factory")
            {
                if (!isThisNodeSpecial)
                {
                    if (turret != null)
                    {
                        ColorTile(fullColor);
                    }
                    else
                    {
                        ColorTile(emptyColor);
                    }
                }
                else
                {
                    ColorTile(fullColor);
                }

            }
            if (gameManager.ClickedBtn.GetTowerPrefab().tag == "CurrencyFactory")
            {
                if (isThisNodeSpecial)
                {
                    if (turret != null)
                    {
                        ColorTile(fullColor);
                    }
                    else
                    {
                        ColorTile(emptyColor);
                    }
                }
                else
                {
                    ColorTile(fullColor);
                }

            }
        }

    }

    private void OnMouseExit()
    {
        colorRenderer.color = startColor;
        if (turret != null)
        {
            isFactoryPlaced = true;
        }
    }

    private void ColorTile(Color newColor)
    {
        colorRenderer.color = newColor;
    }
    private void Update()
    {
        if (isCurrentNodeSelected && turret)
            RefreshCurrentSelectedFactoryHP();
    }

    void PlaceTower()
    {
        if (turret != null)
        {
            Debug.Log("Cant place Here!!!\n");
            return;
        }

        if (gameManager.ClickedBtn != null)
        {
            if (!isThisNodeSpecial && gameManager.ClickedBtn.GetTowerPrefab().tag == "Factory")
            {
                GameObject newTower = Instantiate(gameManager.ClickedBtn.GetTowerPrefab(), gameObject.transform) as GameObject;
                newTower.GetComponentInChildren<SpriteRenderer>().sortingOrder = GetOrderByLayerFromMousePosition();

                turret = newTower;
                RefreshUpdatePanel();
                upgradePanel.DisableUpgradePanel();
                //upgradePanel.EnableUpgradePanel();
                if (turret.tag != "CurrencyFactory")
                {
                    spawnMinions.GetComponent<PreviewRangeOfFactory>().EnableRangePreview();
                    CheckCurrentUpgradeCounter();
                }

                gameManager.BuyTower();
                
            }

            if (isThisNodeSpecial && gameManager.ClickedBtn.GetTowerPrefab().tag == "CurrencyFactory")
            {
                GameObject newTower = Instantiate(gameManager.ClickedBtn.GetTowerPrefab(), gameObject.transform) as GameObject;
                newTower.GetComponentInChildren<SpriteRenderer>().sortingOrder = GetOrderByLayerFromMousePosition();

                turret = newTower;
                gameManager.BuyTower();
                upgradePanel.DisableUpgradePanel();
                
            }
        }


    }

    int GetOrderByLayerFromMousePosition()
    {
        float topPos = Camera.main.orthographicSize;
        Vector3 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int difference = Mathf.RoundToInt(topPos - currMousePos.y);

        return difference;
    }

    void RefreshCurrentSelectedFactoryHP()
    {
        if (turret == null || !isCurrentNodeSelected) return;
        if (turret.tag == "CurrencyFactory") return;
        spawnMinions = turret.GetComponentInChildren<SpawnMinions>();
        upgradePanel.UpdateHP(spawnMinions.factoryHP, spawnMinions.maxFactoryHP);
    }

    void RefreshUpdatePanel()
    {
        if (turret == null) return;
        if (turret.tag != "CurrencyFactory")
        {
            
            spawnMinions = turret.GetComponentInChildren<SpawnMinions>();
            UpgradeAttributes upgradeAttributes = spawnMinions.GetComponent<UpgradeAttributes>();
            upgradePanel.SetTowerAttributes(spawnMinions.turretName, spawnMinions.factoryHP, spawnMinions.maxFactoryHP,
                    spawnMinions.turretDamage, spawnMinions.turretRange,
                    spawnMinions.fireRate, spawnMinions.delayBetweenSpawns);

            upgradePanel.SetUpgradeAttributes(upgradeAttributes.damageUpgrade, upgradeAttributes.rangeUpgrade);
        }
        if(isCalledButton == false)
        {
            
            isCalledButton = true;
        }
        parentNodes.DisableAllNodes();
        parentNodes.DisableAllPreviews();
        isCurrentNodeSelected = true;

        if(turret.tag != "CurrencyFactory")
        {
            spawnMinions.GetComponent<PreviewRangeOfFactory>().SetRangeViewer(spawnMinions.turretRange);
            spawnMinions.GetComponent<PreviewRangeOfFactory>().EnableRangePreview();
        }

        upgradePanel.EnableUpgradePanel();
    }

    void UpgradeDamage()
    {
        if (isCurrentNodeSelected)
        {
            UpgradeAttributes factoryUpgradeAttributes = turret.GetComponentInChildren<UpgradeAttributes>();
            if (factoryUpgradeAttributes.CheckDamageCounter())
            {
                factoryUpgradeAttributes.UpgradeDamage();
                RefreshUpdatePanel();
                upgradePanel.EnableUpgradePanel();
            }
            else
            {
                damageIncreaseButton.enabled = false;
                upgradePanel.CheckDamageText(false);
            }
        }
    }

    void UpgradeRange()
    {
        if(isCurrentNodeSelected)
        {
            UpgradeAttributes factoryUpgradeAttributes = turret.GetComponentInChildren<UpgradeAttributes>();
            if(factoryUpgradeAttributes.CheckRangeCounter())
            {
                factoryUpgradeAttributes.UpgradeRange();
                RefreshUpdatePanel();
                upgradePanel.EnableUpgradePanel();
            }
            else
            {
                rangeIncreaseButton.enabled = false;
                upgradePanel.CheckRangeText(false);
            }
        }
    }

    void HealCurrentFactory()
    {
        if(isCurrentNodeSelected)
        {
            UpgradeAttributes factoryUpgradeAttributes = turret.GetComponentInChildren<UpgradeAttributes>();
            factoryUpgradeAttributes.HealFactory();
            RefreshUpdatePanel();
            upgradePanel.EnableUpgradePanel();
        }
    }

    //void UpgradeMaxHP()
    //{
    //    if (isCurrentNodeSelected)
    //    {
    //        UpgradeAttributes factoryUpgradeAttributes = turret.GetComponentInChildren<UpgradeAttributes>();
    //        if (factoryUpgradeAttributes.CheckMaxHPCounter())
    //        {
    //            factoryUpgradeAttributes.IncreaseMaxFactoryHP();
    //            RefreshUpdatePanel();
    //            upgradePanel.EnableUpgradePanel();
    //        }
    //        else
    //        {
    //            increaseMaxFactoryUpgradeButton.enabled = false;
    //            upgradePanel.CheckMaxHpText(false);
    //        }
    //    }

    //}

  void CheckCurrentUpgradeCounter()
    {
        UpgradeAttributes upgradeAttributes = turret.GetComponentInChildren<UpgradeAttributes>();
        bool checkDamage = upgradeAttributes.CheckDamageCounter();
        bool checkRange = upgradeAttributes.CheckRangeCounter();
        bool checkMaxHP = upgradeAttributes.CheckMaxHPCounter();

        upgradePanel.CheckDamageText(checkDamage);
        upgradePanel.CheckRangeText(checkRange);
        //upgradePanel.CheckMaxHpText(checkMaxHP);

        if (checkDamage) damageIncreaseButton.enabled = true;
        else damageIncreaseButton.enabled = false;

        if (checkRange) rangeIncreaseButton.enabled = true;
        else rangeIncreaseButton.enabled = false;

        if (checkMaxHP) increaseMaxFactoryUpgradeButton.enabled = true;
        else increaseMaxFactoryUpgradeButton.enabled = false;
    }

   void DeleteCurrentFactory()
    {
        //Destroy and Add Money
        if(isCurrentNodeSelected)
        {
            float selectedFactoryCost = GetComponentInChildren<SpawnMinions>().factoryCost;
            selectedFactoryCost /= 4;
            gameManager.Currency += selectedFactoryCost;
            Destroy(GetComponentInChildren<Tower>().gameObject);
            turret = null;
            upgradePanel.DisableUpgradePanel();
        }
    }

    public void DestroyCurrentFactory()
    {
        Destroy(GetComponent<Transform>().GetChild(0).gameObject);
        turret = null;
        upgradePanel.DisableUpgradePanel();
    }

}
