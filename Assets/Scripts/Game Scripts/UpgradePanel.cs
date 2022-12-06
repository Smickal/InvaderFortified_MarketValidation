using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradePanel : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Description Panel")]
    [SerializeField]private TextMeshProUGUI towerName;
    //[SerializeField] private TextMeshProUGUI factoryHP;
    [SerializeField] private TextMeshProUGUI minionDamage;
    [SerializeField] private TextMeshProUGUI minionFireRate;
    //[SerializeField] private Text recoveryRate;
    [SerializeField] private TextMeshProUGUI rangeText;
    [SerializeField] GameObject upgradeGroups;

    [Header("Upgrade Buttons")]
    [SerializeField] Button damageIncreaseButton;
    [SerializeField] Button rangeIncreaseButton;
    //[SerializeField] Button recoverHealthButton;
    //[SerializeField] Button increaseMaxFactoryUpgradeButton;
    //[SerializeField] Button cancelButton;

    [Header("Upgrade Text_Attack")]
    [SerializeField] GameObject damageText;
    [SerializeField] TextMeshProUGUI attackNumText;
    [SerializeField] GameObject damageTextDisabled;

    [Header("Upgrade Text_Range")]
    [SerializeField] GameObject rangeTexT;
    [SerializeField] TextMeshProUGUI rangeNumText;
    [SerializeField] GameObject rangeTextDisabled;

    //[SerializeField] GameObject maxHPText;
    //[SerializeField] GameObject maXHPTextDisabled;

    public bool isUpgradePanelActivated = false;
    allNodes allnode;
    RetractableController retractableController;

    private void Awake()
    {
        allnode = FindObjectOfType<allNodes>();
        retractableController = GetComponent<RetractableController>();

        damageTextDisabled.SetActive(false);
        rangeTextDisabled.SetActive(false);
        //maXHPTextDisabled.SetActive(false);
    }
    public void SetTowerAttributes(string towerName,
        float minionDamage, float turretRange,float minionFireRate)
    {
        this.towerName.text = towerName;
        this.minionDamage.text = minionDamage.ToString();
        this.minionFireRate.text =  minionFireRate.ToString();
        rangeText.text = turretRange.ToString();
        //recoveryRate.text = "Recovery rate: " + minionRecoveryRate.ToString() + " seconds";
    }
    
    public void SetUpgradeAttributes(float damageNum, float rangeNum)
    {
        string tempAttstr = "DMG +" + damageNum;
        string tempRangeStr = "Range +" + rangeNum;

        attackNumText.text = tempAttstr;
        rangeNumText.text = tempRangeStr;
    }


    

    public void EnableUpgradePanel()
    {
        if (!isUpgradePanelActivated)
            retractableController.TriggerRetracable();

        isUpgradePanelActivated = true;
        //cancelButton.gameObject.SetActive(true);
        //upgradeGroups.SetActive(true);
    }

    public void DisableUpgradePanel()
    {
        if (isUpgradePanelActivated)
            retractableController.TriggerRetracable();

        isUpgradePanelActivated = false;
        allnode.DisableAllNodes();
    }

    public void EnableUpgradePanelWhenButtoneClicked()
    {
        if (!isUpgradePanelActivated)
            retractableController.TriggerRetracable();

        isUpgradePanelActivated = true;
        //cancelButton.gameObject.SetActive(false);
        //upgradeGroups.SetActive(false);
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
