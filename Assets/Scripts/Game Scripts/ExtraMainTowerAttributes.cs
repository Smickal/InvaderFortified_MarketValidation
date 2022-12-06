using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraMainTowerAttributes : MonoBehaviour
{
    [SerializeField]float mainTowerHp;

    public float startingHP;


    private void Start()
    {
        startingHP = mainTowerHp;
    }

    private void Update() 
    {
        CheckGameOver();
    }

    void CheckGameOver()
    {
        if(mainTowerHp <= 0)
        {
            Debug.Log("GameOver!!");
            mainTowerHp = 0;
        }
    }

    public void ReduceMainTowerHp(float value)
    {
        mainTowerHp -= value;
    }

    public float GetMainTowerHP()
    {
        return mainTowerHp;
    }
}
