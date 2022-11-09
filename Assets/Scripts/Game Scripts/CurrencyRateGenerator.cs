using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyRateGenerator : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField]private float setStartingRate = 5f;
    private float currentRate = 10f;
    
    [Header("Localization")]
    [SerializeField]GameManager gameManager;
    [SerializeField]TextMeshProUGUI rateCurrencyText;
    [SerializeField] WaveSpawner waveSpawner;
    
    float rateCoundownTimer = 1;

    private void Awake()
    {
        CurrentRate = setStartingRate;
    }

    public float CurrentRate
    {
        get
        {
            return currentRate;
        }
        set
        {
            this.currentRate = value;

        }
    }

    void Update()
    {
        UpdateCurrentCurrency();
    }

    void UpdateCurrentCurrency()
    {
        rateCoundownTimer -= Time.deltaTime;
        if (rateCoundownTimer <= 0 && waveSpawner.inGame)
        {
            gameManager.AddCurrency(CurrentRate);
            rateCoundownTimer = 1;
            rateCurrencyText.text = currentRate.ToString()+ "/Second";
        }
    }

    public string GetCurrentCurrency()
    {
        return currentRate.ToString();
    }

   
}
