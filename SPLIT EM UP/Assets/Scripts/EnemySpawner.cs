using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] private List<GameObject> spawnPoints;
    private System.Random rnd = new System.Random();

    [Range(0, 10)]
    [SerializeField] private int numOfEnemies;
    private int currentEnemiesSpawned = 0;

    [Header("Spawning System")]
    [Range(0, 3f)]
    [SerializeField] private float respawnTime;
    private float curRespawnTime = 0f;

    private void Start()
    {
        StartCoroutine(StartRespawningLogic());
    }

    IEnumerator StartRespawningLogic()
    {
        WaitForSeconds wait = new WaitForSeconds(respawnTime);
        while (currentEnemiesSpawned < numOfEnemies)
        {
            GameObject spawnPoint = ChooseRandomSpawnPoint();
            Instantiate(enemy, spawnPoint.transform.position, Quaternion.identity);
            ++currentEnemiesSpawned;
            Debug.Log("Created");
            yield return wait;
        }
    }

    GameObject ChooseRandomSpawnPoint()
    {
        int index = rnd.Next(0, spawnPoints.Count);
        return spawnPoints[index];
    }
}
