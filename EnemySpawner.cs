using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyPrefab
    {
        public GameObject prefab;
        [Range(0, 1)]
        public float spawnChance;
    }

    public EnemyPrefab[] enemyPrefabs;
    public GameObject[] spawnPoints; // Array of spawn points
    public float spawnDelay = 3f;

    private void Start()
    {
        // Start spawning enemies
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        // Choose a random enemy prefab to spawn
        GameObject enemyToSpawn = ChooseRandomEnemyPrefab();

        // Get a random spawn point from the array
        GameObject spawnPoint = GetRandomSpawnPoint();

        // Spawn the enemy at the chosen spawn point
        Instantiate(enemyToSpawn, spawnPoint.transform.position, Quaternion.identity);
    }

    private GameObject ChooseRandomEnemyPrefab()
    {
        // Calculate total spawn chance
        float totalSpawnChance = 0f;
        foreach (var enemyPrefab in enemyPrefabs)
        {
            totalSpawnChance += enemyPrefab.spawnChance;
        }

        // Get a random value within the total spawn chance
        float randomValue = Random.value * totalSpawnChance;

        // Iterate through enemy prefabs and choose one based on spawn chance
        foreach (var enemyPrefab in enemyPrefabs)
        {
            if (randomValue < enemyPrefab.spawnChance)
            {
                return enemyPrefab.prefab;
            }
            randomValue -= enemyPrefab.spawnChance;
        }

        // This should never happen, but just in case
        return null;
    }

    private GameObject GetRandomSpawnPoint()
    {
        // Choose a random spawn point from the array
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex];
    }
}
