using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Description Panel")]
    [SerializeField]private Text towerName;
    [SerializeField] private Text factoryHP;
    [SerializeField] private Text minionDamage;
    [SerializeField] private Text minionFireRate;
    //[SerializeField] private Text recoveryRate;
    [SerializeField] private Text rangeText;
    [SerializeField] GameObject upgradeGroups;

    [Header("Upgrade Buttons")]
    [SerializeField] Button damageIncreaseButton;
    [SerializeField] Button rangeIncreaseButton;
    //[SerializeField] Button recoverHealthButton;
    //[SerializeField] Button increaseMaxFactoryUpgradeButton;
    [SerializeField] Button cancelButton;

    [Header("Upgrade Text_Attack")]
    [SerializeField] GameObject damageText;
    [SerializeField] Text attackNumText;
    [SerializeField] GameObject damageTextDisabled;

    [Header("Upgrade Text_Range")]
    [SerializeField] GameObject rangeTexT;
    [SerializeField] Text rangeNumText;
    [SerializeField] GameObject rangeTextDisabled;

    //[SerializeField] GameObject maxHPText;
    //[SerializeField] GameObject maXHPTextDisabled;

    public bool isUpgradePanelActivated = false;
    allNodes allnode;

    private void Awake()
    {
        allnode = FindObjectOfType<allNodes>();
        damageTextDisabled.SetActive(false);
        rangeTextDisabled.SetActive(false);
        //maXHPTextDisabled.SetActive(false);
    }
    public void SetTowerAttributes(string towerName, float factoryHP, float maxFactoryHP,
        float minionDamage, float turretRange,float minionFireRate,
        float minionRecoveryRate)
    {
        this.towerName.text = towerName;
        this.factoryHP.text = "HP: " + factoryHP.ToString() + "/ " + maxFactoryHP.ToString();
        this.minionDamage.text = "Damage: " + minionDamage.ToString();
        this.minionFireRate.text = "Fire rate: " + minionFireRate.ToString();
        rangeText.text = "Range: " + turretRange.ToString();
        //recoveryRate.text = "Recovery rate: " + minionRecoveryRate.ToString() + " seconds";
    }
    
    public void SetUpgradeAttributes(float damageNum, float rangeNum)
    {
        string tempAttstr = "Attack +" + damageNum;
        string tempRangeStr = "Range +" + rangeNum;

        attackNumText.text = tempAttstr;
        rangeNumText.text = tempRangeStr;
    }


    public void UpdateHP(float factoryHP, float maxFactoryHP)
    {
        if(isUpgradePanelActivated)
        {
            this.factoryHP.text = "HP: " + factoryHP.ToString() + "/ " + maxFactoryHP.ToString();
        }
    }

    public void EnableUpgradePanel()
    {
        gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);
        isUpgradePanelActivated = true;
        upgradeGroups.SetActive(true);
    }

    public void DisableUpgradePanel()
    {
        gameObject.SetActive(false);
        isUpgradePanelActivated = false;
        allnode.DisableAllNodes();
    }

    public void EnableUpgradePanelWhenButtoneClicked()
    {
        gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(false);
        isUpgradePanelActivated = true;
        upgradeGroups.SetActive(false);
    }

   public void CheckDamageText(bool check)
    {
        if(check)
        {
            damageText.SetActive(true);
            damageTextDisabled.SetActive(false);
        }else
        {
            damageText.SetActive(false);
            damageTextDisabled.SetActive(true);
        }
    }

    public void CheckRangeText(bool check)
    {
        if (check)
        {
            rangeTexT.SetActive(true);
            rangeTextDisabled.SetActive(false);
        }
        else
        {
            rangeTexT.SetActive(false);
            rangeTextDisabled.SetActive(true);
        }
    }

    //public void CheckMaxHpText(bool check)
    //{
    //    if (check)
    //    {
    //        maxHPText.SetActive(true);
    //        maXHPTextDisabled.SetActive(false);
    //    }
    //    else
    //    {
    //        maxHPText.SetActive(false);
    //        maXHPTextDisabled.SetActive(true);
    //    }
    //}


}
