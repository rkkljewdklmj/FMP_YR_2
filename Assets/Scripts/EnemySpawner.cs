using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Enemy to spawn
    public Transform spawnPoint;   // Where enemies spawn
    public float timeBetweenWaves = 5f; // Delay before new wave
    public float spawnInterval = 1f; // Time between enemy spawns

    private int waveNumber = 1; // Start with 1 enemy

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true) // Infinite waves (remove or modify for limited waves)
        {
            yield return StartCoroutine(SpawnEnemies(waveNumber)); // Wait until all enemies spawn
            yield return new WaitForSeconds(timeBetweenWaves); // Wait before next wave

            waveNumber++; // Increase difficulty
        }
    }

    IEnumerator SpawnEnemies(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(spawnInterval); // Wait before spawning next enemy
        }
    }
}
