using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField]private GameObject tower;
    [SerializeField]private Sprite sprite;
    [SerializeField]private int towerCost;
    [SerializeField]private TextMeshProUGUI factoryCostText;
    [SerializeField]private float turretRange;

    private void Start()
    {
        factoryCostText.text = GetTowerCost().ToString();
    }

    public GameObject GetTowerPrefab()
    {
        return tower;
    }
    public void PurchaseStandardTurret()
    {
        Debug.Log("Standard Turret purchased");
    }

    public Sprite GetSpriteOfTower()
    {
        return sprite;
    }

    public int GetTowerCost()
    {
        return towerCost;
    }

    public float GetTowerRange()
    {
        return turretRange;
    }
}
