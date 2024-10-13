using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class WavesController : MonoBehaviour
{
    public delegate void WaveStartedHandler(int wave);
    public WaveStartedHandler WavesStarted;

    public delegate void WavesFinishedHandler();
    public WavesFinishedHandler WavesFinished;

    public delegate void EnemiesKilledHandler();
    public EnemiesKilledHandler EnemiesKilled;

    LevelDataSO levelData;

    WaveDataSO wavesData;
    WaveData currentWaveData;

    public Transform spawPoint;
    private int waveIndex = 0;

    [SerializeField] List<CharacterConfig> enemies = new List<CharacterConfig>();
    [SerializeField] int enemiesKilled = 0;

    float enemySpawnCooldown;
    float nextWaveCooldown;
    float enemiesToInstantiate;

    public void InitializeComponent()
    {
        levelData = ServiceLocator.GetService<GameManager>().LevelData;
        wavesData = levelData.Waves;

        CharacterConfig.OnCharacterDestroyed += EnemyDestroyed;
        CharacterConfig.OnCharacterKilled += EnemyKilled;
    }

    public void SpawnWave()
    {
        currentWaveData = wavesData.WaveDataList[waveIndex];
        enemiesToInstantiate = currentWaveData.EnemiesAmmount;
        enemies.Clear();
        enemySpawnCooldown = 0.1f;
        WavesStarted?.Invoke(waveIndex + 1);
    }

    private void Update()
    {
        if (currentWaveData != null)
        {
            if (nextWaveCooldown > 0)
            {
                nextWaveCooldown -= Time.deltaTime;
                if (nextWaveCooldown <= 0)
                {
                    SpawnWave();
                }
            }

            if (enemySpawnCooldown > 0)
            {
                enemySpawnCooldown -= Time.deltaTime;
                if (enemySpawnCooldown <= 0)
                {
                    if (enemiesToInstantiate > 0)
                    {
                        SpawnRandomEnemy();
                        enemiesToInstantiate--;
                        enemySpawnCooldown = Random.Range(currentWaveData.MinRateTime, currentWaveData.MaxRateTime);
                    }
                    else
                    {
                        Debug.Log("Wave instanciada con exito");
                    }
                }
            }
        }
    }

    public void SpawnRandomEnemy()
    {
        int randomEnemy = Random.Range(0, currentWaveData.Enemies.Count);
        CharacterSO characterData = currentWaveData.Enemies[randomEnemy];
        GameObject spawnedEnemy = SpawnEnemy((EnemyDataSO)characterData);

        if (spawnedEnemy)
        {
            CharacterConfig controller = spawnedEnemy.GetComponent<CharacterConfig>();
            controller.ConfigureCharacter(characterData);
            enemies.Add(controller);
        }
    }
    public void NextWave()
    {
        enemySpawnCooldown = -1;
        nextWaveCooldown = currentWaveData.NextWaveTime;
        waveIndex++;
    }

    public void EnemyKilled(CharacterConfig character)
    {
        enemiesKilled++;

        if (enemies.Count <= 0)
        {
            EnemiesKilled?.Invoke();
        }
    }

    public void EnemyDestroyed(CharacterConfig character)
    {
        if (enemies.Contains(character))
        {
            enemies.Remove(character);

            if (enemies.Count <= 0)
            {
                if (waveIndex + 1 < wavesData.WaveDataList.Count)
                {
                    NextWave();
                }

                else
                {
                    WavesFinished?.Invoke();
                }
            }
        }
    }

    public int GetEnemiesKilled()
    {
        return enemiesKilled;
    }

    GameObject SpawnEnemy(EnemyDataSO enemyData)
    {
        return Instantiate(enemyData.Prefab, spawPoint.position, spawPoint.rotation);
    }

    private void OnDisable()
    {
        CharacterConfig.OnCharacterDestroyed -= EnemyDestroyed;
        CharacterConfig.OnCharacterKilled -= EnemyKilled;
    }
}
