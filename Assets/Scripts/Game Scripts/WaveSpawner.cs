using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] enemyPrefab;
    public Transform spawnLocation;

    [Header("Variables")]
    public float timeBetweenWaves = 4f;
    public float timeDelayBetweenEnemies = 1f;
    public int enemiesLeft = 10;

    [Header("Localization")]
    public TextMeshProUGUI currentWaveText;
    public TextMeshProUGUI enemiesLeftText;
    public GameObject waveSpawnerButton;
    public DynamicDifficulityAdjustment dda;


    int waveNumber = 1;
    int startingEnemies;
    float stopwatch = 0;
    float currentCheckedTime;
    bool timeStamp = false;
    int currentMaxEnemy;

    public int getCurrentEnemiesInScreen = 0;

    public bool isWaveStarting = false;
    public bool inGame = false;
    public int EnemiesLeft
    {
        get
        {
            return enemiesLeft;
        }
        set
        {
            this.enemiesLeft = value;

        }
    }

    private void Start()
    {
        enemiesLeftText.text = "Enemies left: " + enemiesLeft.ToString();
        currentWaveText.text = "WAVE: " + (waveNumber).ToString();
        startingEnemies = enemiesLeft;
        currentMaxEnemy = enemyPrefab.Length;
    }

    private void Update()
    {
        BeganWave();
        WaveEnded();
        CountTime();
    }

    private void WaveEnded()
    {
        if (!isWaveStarting && (getCurrentEnemiesInScreen == 0 && enemiesLeft == 0))
        {
            inGame = false;
            waveSpawnerButton.gameObject.SetActive(true);
            enemiesLeftText.text = "Enemies left: " + enemiesLeft.ToString();
            if (!timeStamp)
            {
                currentCheckedTime = stopwatch;
                timeStamp = true;
            }
            FindObjectOfType<CheckGameWin>().CheckForEndingWave(waveNumber);
        }
    }

    private void BeganWave()
    {
        if (isWaveStarting)
        {
            inGame = true;
            getCurrentEnemiesInScreen = 0;
            waveSpawnerButton.gameObject.SetActive(false);
            currentWaveText.text = "WAVE: " + (waveNumber).ToString();
            CalculateEnemiesRemainingInThisWave();
            enemiesLeftText.text = "Enemies left: " + enemiesLeft.ToString();
            stopwatch = 0;
            timeStamp = false;
            StartCoroutine(SpawnWave());
            
        }
    }



    public void CalculateEnemiesRemainingInThisWave()
    {
        if(waveNumber == 1)
         enemiesLeft = startingEnemies;
        else
        {
            enemiesLeft = startingEnemies + dda.GetIncrementSpawnRate(currentCheckedTime,FindObjectOfType<ExtraMainTowerAttributes>().MainTowerHp);
            startingEnemies = enemiesLeft;
        }
        
    }

    IEnumerator SpawnWave()
    {
        isWaveStarting = false;
        for (int i = enemiesLeft; i > 0; i--)
        {
            int randEnemy = GetEnemyPrefab();
            Instantiate(enemyPrefab[randEnemy],spawnLocation.position, spawnLocation.localRotation);
            enemiesLeft--;
            enemiesLeftText.text = "Enemies left: " + enemiesLeft.ToString();
            getCurrentEnemiesInScreen++;
            float rand = Random.Range(0.2f, timeDelayBetweenEnemies);
            yield return new WaitForSeconds(rand);
        }
        waveNumber++;
        
    }

    int GetEnemyPrefab()
    {
        return Random.Range(0, currentMaxEnemy);
    }

    public void StartWave()
    {
        isWaveStarting = true;
    }

    public void CountTime()
    {
        stopwatch += Time.deltaTime;
    }

    public int GetCurrentWave()
    {
        return waveNumber;
    }

}
