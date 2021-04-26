using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private int enemyCount = 3;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    private int spawnedEnemiesCount = 0;

    public void Activate()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (spawnedEnemiesCount < enemyCount)
        {
            Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
            Instantiate(enemyToSpawn, spawnPosition, quaternion.identity);
            //Force move to player
            yield return new WaitForSeconds(spawnInterval);
            spawnedEnemiesCount++;
        }
    }
}