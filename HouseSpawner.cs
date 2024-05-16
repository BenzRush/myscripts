using UnityEngine;
using System.Collections.Generic;

public class HouseSpawner : MonoBehaviour
{
    public GameObject[] housePrefabs; // Array of house prefabs
    public GameObject[] spawnPoints; // Array of spawn points
    public float spawnDelay = 3f;

    private void Start()
    {
        // Start spawning houses
        SpawnHouse();
    }

    private void SpawnHouse()
    {
        // Choose a random house prefab to spawn
        GameObject houseToSpawn = GetRandomHousePrefab();

        // Get a random spawn point from the array
        GameObject spawnPoint = GetRandomSpawnPoint();

        // Spawn the house at the chosen spawn point
        Instantiate(houseToSpawn, spawnPoint.transform.position, Quaternion.identity);
    }

    private GameObject GetRandomHousePrefab()
    {
        // Choose a random house prefab from the array
        int randomIndex = Random.Range(0, housePrefabs.Length);
        return housePrefabs[randomIndex];
    }

    private GameObject GetRandomSpawnPoint()
    {
        // Choose a random spawn point from the array
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex];
    }
}
