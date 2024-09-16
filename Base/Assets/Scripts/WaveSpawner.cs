using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform spawPoint;

    [Range(5f, 25f)]
    public float timeBetweenWaves = 5f;

    private float enemyDistance;

    public float numOfEnemies;

    private float countdown = 2f;
    public TextMeshProUGUI countdown_txt;

    private int waveIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity); 
        countdown_txt.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        numOfEnemies = waveIndex * waveIndex + 1;
        for (int i = 0; i < numOfEnemies; i++)
        {
            SpawnEnemy();
            enemyDistance = Random.Range(1f, 2.5f); 
            yield return new WaitForSeconds(enemyDistance);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab,spawPoint.position,spawPoint.rotation);
    }
}
