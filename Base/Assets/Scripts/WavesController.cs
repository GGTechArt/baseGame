using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WavesController : MonoBehaviour
{
    public delegate void WavesFinishedHandler();
    public WavesFinishedHandler WavesFinished;

    LevelDataSO levelData;

    WaveDataSO wavesData;
    WaveData currentWaveData;

    public Transform spawPoint;
    private int waveIndex = 0;

    List<CharacterConfig> enemies = new List<CharacterConfig>();
    int enemiesRemaining;

    public void InitializeComponent()
    {
        levelData = ServiceLocator.GetService<GameManager>().LevelData;
        wavesData = levelData.Waves;

        CharacterConfig.OnCharacterDestroyed += EnemyKilled;
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
                CharacterConfig controller = spawnedEnemy.GetComponent<CharacterConfig>();
                controller.ConfigureCharacter(characterData);
                enemies.Add(controller);
                float enemyDistance = Random.Range(currentWaveData.MinRateTime, currentWaveData.MaxRateTime);
                yield return new WaitForSeconds(enemyDistance);
            }
        }

        else
        {
            WavesFinished?.Invoke();
            Debug.Log("Finalizadas Waves");
        }
    }

    public void NextWave()
    {
        waveIndex++;
        SpawnWave();
    }

    public void EnemyKilled(CharacterConfig character)
    {
        if (enemies.Contains(character))
        {
            enemies.Remove(character);

            if (enemies.Count <= 0)
            {
                NextWave();
            }
        }
    }

    GameObject SpawnEnemy(EnemyDataSO enemyData)
    {
        return Instantiate(enemyData.Prefab, spawPoint.position, spawPoint.rotation);
    }
}
