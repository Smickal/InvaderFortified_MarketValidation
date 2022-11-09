using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGameWin : MonoBehaviour
{
    // Start is called before the first frame update

    WaveSpawner wSpawner;
    [SerializeField] int endWave;
    [SerializeField] GameObject winScreen;


    private void Awake()
    {
        wSpawner = FindObjectOfType<WaveSpawner>();
    }


    // Update is called once per frame

    public void CheckForEndingWave(int currentWave)
    {
        if (currentWave > endWave)
        {
            winScreen.SetActive(true);
        }
    }

    public void DisableWinScreen()
    {
        winScreen.SetActive(false);
    }

}
