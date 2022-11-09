using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class movingIconsBottomBar : MonoBehaviour
{
    [Header("Currency Text")]

    [SerializeField] private TextMeshProUGUI pos2currencyText;
    [SerializeField] private TextMeshProUGUI pos2currencyRateText;

    UpgradePanel upgradePanel;
    GameManager gameManager;
    CurrencyRateGenerator currencyRateGenerator;


    private void Awake()
    {
        upgradePanel = FindObjectOfType<UpgradePanel>();
        gameManager = FindObjectOfType<GameManager>();
        currencyRateGenerator = FindObjectOfType<CurrencyRateGenerator>();

        pos2currencyRateText.gameObject.SetActive(false);
        pos2currencyText.gameObject.SetActive(false);
    }

    private void Update()
    {
        pos2currencyText.text = gameManager.Currency.ToString() + " $";
        pos2currencyRateText.text = currencyRateGenerator.GetCurrentCurrency() + "/Second";
        MoveText();
    }

    void MoveText()
    {
        if(upgradePanel.isUpgradePanelActivated == true)
        {
            pos2currencyText.gameObject.SetActive(true);
            pos2currencyRateText.gameObject.SetActive(true);
        }
        else
        {
            pos2currencyRateText.gameObject.SetActive(false);
            pos2currencyText.gameObject.SetActive(false);
        }
    }
    

}
