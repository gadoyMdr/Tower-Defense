using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public Action<float, float> EnemiesHealthUpdated;
    public Action<string, Color> WaveInfo;
    public Action<bool> ActivateInfoText;
    public Action<bool> EnemyBeingSpawnedUpdate;
    public Action<string, bool> BigText;

    [SerializeField]
    [Range(2f, 50f)]
    private float timeBetweenWaves = 5;

    [SerializeField]
    [Range(2f, 10f)]
    private float timeStartBattle = 3;

    [SerializeField]
    [Range(2f, 10f)]
    private float timeGameOver = 3;


    private int waveCounter = 0;
    private Level levelData;

    private List<Health> enemiesHealth = new List<Health>();

    private void Start()
    {
        levelData = FindObjectOfType<Level>();
        StartCoroutine(GameCoroutine());
    }


    IEnumerator GameCoroutine()
    {
        yield return StartCoroutine(StartGame());

        for (int i = 0; i < levelData.waves.Count; i++)
        {
            StartCoroutine(SpawnWaveCoroutine(waveCounter));
            yield return StartCoroutine(WaitForWaveToFinish());

            if (!Death.isDead && waveCounter == levelData.waves.Count)
                StartCoroutine(Win());

            yield return StartCoroutine(WaitBetweenWaves());
        }
        
        

    }

    IEnumerator StartGame()
    {
        BigText?.Invoke(GameSaveManager.Instance.levelStats.name, true);
        yield return new WaitForSeconds(timeStartBattle);
        BigText?.Invoke(GameSaveManager.Instance.levelStats.name, false);
    }

    IEnumerator WaitForWaveToFinish()
    {
        yield return new WaitForSeconds(1);
        while (enemiesHealth.Sum(x => x.currentHealth) != 0)
        {
            yield return null;
        }
    }

    IEnumerator WaitBetweenWaves()
    {

        float normalizedTime = 0;
        ActivateInfoText?.Invoke(true);
        while (normalizedTime <= timeBetweenWaves)
        {
            normalizedTime += Time.deltaTime;
            WaveInfo?.Invoke($"Time until next wave {(timeBetweenWaves - normalizedTime):F0}", Color.red);
            yield return null;
        }

        ActivateInfoText?.Invoke(false);
    }

    IEnumerator SpawnWaveCoroutine(int wave)
    {
        enemiesHealth.Clear();

        WaveInfo?.Invoke($"Enemies Incoming", Color.red);
        ActivateInfoText?.Invoke(true);
        EnemyBeingSpawnedUpdate?.Invoke(true);

        for (int i = 0; i < levelData.waves[wave].enemyCount; i++)
        {
            Health enemyHealth = Instantiate(levelData.waves[wave].randomEnemies.GetRandomObject(), levelData.spawnPoint.position, levelData.spawnPoint.rotation)
                .GetComponent<Health>();

            enemiesHealth.Add(enemyHealth);
            enemyHealth.HealthUpdatedAction += UpdateWaveProgression;

            yield return new WaitForSeconds(levelData.waves[wave].spawnTime);
        }
        EnemyBeingSpawnedUpdate?.Invoke(false);
        ActivateInfoText?.Invoke(false);
        waveCounter++;
    }

    void UpdateWaveProgression(float a,float b)
    {
        EnemiesHealthUpdated?.Invoke(enemiesHealth.Sum(x => x.currentHealth), enemiesHealth.Sum(x => x.startHealth));
    }

    IEnumerator Win()
    {
        BigText?.Invoke("You win!", true);
        GameSaveManager.Instance.levelStats.done = true;
        GameSaveManager.Instance.SaveGame();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainMenu");
    }
}
