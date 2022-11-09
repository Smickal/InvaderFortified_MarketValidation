using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyTower : MonoBehaviour
{
    [SerializeField]private float addCurrencyRate = 10f;

    CurrencyRateGenerator currencyRateGenerator;

    private void Start() 
    {
        currencyRateGenerator = FindObjectOfType<CurrencyRateGenerator>();
        currencyRateGenerator.CurrentRate += addCurrencyRate;
    }

    
}
