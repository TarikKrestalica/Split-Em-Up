using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] private List<GameObject> spawnLocations;
    private System.Random rnd = new System.Random();

    [Range(0, 10)]
    [SerializeField] private int numOfEnemies;
    private int currentEnemiesSpawned = 0;

    [Header("Spawning System")]
    [Range(0, 5f)]
    [SerializeField] private float delay;

    private void Start()
    {
        StartCoroutine(StartRespawningLogic());
    }

    IEnumerator StartRespawningLogic()
    {
        WaitForSeconds wait = new WaitForSeconds(delay);
        while (currentEnemiesSpawned < numOfEnemies)
        {
            GameObject spawnPoint = ChooseRandomSpawnPoint();
            Instantiate(enemy, spawnPoint.transform.position, Quaternion.identity);
            ++currentEnemiesSpawned;
            yield return wait;
        }
    }

    GameObject ChooseRandomSpawnPoint()
    {
        int index = rnd.Next(0, spawnLocations.Count);
        return spawnLocations[index];
    }
}
