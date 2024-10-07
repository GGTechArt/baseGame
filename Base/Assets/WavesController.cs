using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WavesController : MonoBehaviour
{
    WaveDataSO wavesData;
    WaveData currentWaveData;

    public Transform spawPoint;
    private int waveIndex = 0;

    List<IDamageable> enemies = new List<IDamageable>();
    int enemiesRemaining;

    private void Start()
    {
        //wavesData = ServiceLocator.GetService<GameManager>().LevelData.Waves;
    }

    public void SpawnWave()
    {
        StartCoroutine(SpawnWaveCoroutine());
    }
    IEnumerator SpawnWaveCoroutine()
    {
        wavesData = ServiceLocator.GetService<GameManager>().LevelData.Waves;

        if (waveIndex < wavesData.WaveDataList.Count)
        {
            enemies.Clear();
            currentWaveData = wavesData.WaveDataList[waveIndex];

            for (int i = 0; i < currentWaveData.EnemiesAmmount; i++)
            {
                int randomEnemy = Random.Range(0, currentWaveData.Enemies.Count);
                //enemies.Add(SpawnEnemy(currentWaveData.Enemies[randomEnemy]));
                float enemyDistance = Random.Range(currentWaveData.MinRateTime, currentWaveData.MaxRateTime);
                yield return new WaitForSeconds(enemyDistance);
            }
        }
    }

    public void NextWave()
    {
        waveIndex++;
        SpawnWave();
    }

    public void EnemyKilled(GameObject enemy)
    {
        //if (enemies.Contains(enemy))
        //{
        //    enemies.Remove(enemy);
        //}
    }

    GameObject SpawnEnemy(EnemyDataSO enemyData)
    {
        return Instantiate(enemyData.Prefab, spawPoint.position, spawPoint.rotation);
    }
}
