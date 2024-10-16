using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WavesController : MonoBehaviour
{
    public delegate void WavesFinishedHandler();
    public WavesFinishedHandler WavesFinished;

    public delegate void EnemiesKilledHandler();
    public EnemiesKilledHandler EnemiesKilled;

    LevelDataSO levelData;

    WaveDataSO wavesData;
    WaveData currentWaveData;

    public Transform spawPoint;
    private int waveIndex = 0;

    List<CharacterConfig> enemies = new List<CharacterConfig>();
    [SerializeField] int enemiesKilled = 0;

    public void InitializeComponent()
    {
        levelData = ServiceLocator.GetService<GameManager>().LevelData;
        wavesData = levelData.Waves;

        CharacterConfig.OnCharacterDestroyed += EnemyDestroyed;
    }

    public void SpawnWave()
    {
        StartCoroutine(SpawnWaveCoroutine());
    }

    IEnumerator SpawnWaveCoroutine()
    {
        if (waveIndex < wavesData.WaveDataList.Count)
        {
            enemies.Clear();
            currentWaveData = wavesData.WaveDataList[waveIndex];

            for (int i = 0; i < currentWaveData.EnemiesAmmount; i++)
            {
                int randomEnemy = Random.Range(0, currentWaveData.Enemies.Count);
                CharacterSO characterData = currentWaveData.Enemies[randomEnemy];
                GameObject spawnedEnemy = SpawnEnemy((EnemyDataSO)characterData);
                if (spawnedEnemy)
                {
                    CharacterConfig controller = spawnedEnemy.GetComponent<CharacterConfig>();
                    controller.ConfigureCharacter(characterData);
                    controller.Damageable.OnDeath += EnemyKilled;
                    enemies.Add(controller);
                }

                float enemyDistance = Random.Range(currentWaveData.MinRateTime, currentWaveData.MaxRateTime);
                yield return new WaitForSeconds(enemyDistance);
            }
        }

        else
        {
            Debug.Log("Finalizadas Waves");
        }
    }

    public void NextWave()
    {
        waveIndex++;
        SpawnWave();
    }

    public void EnemyKilled()
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
            character.Damageable.OnDeath -= EnemyKilled;
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
}
