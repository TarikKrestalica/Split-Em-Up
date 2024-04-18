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

            // Stop camera when fighting enemies
            yield return wait;
            if (currentEnemiesSpawned == numOfEnemies)  // Won their fighting zone, move to next stages
            {
                GameManager.player.SetAtFightingZone(false);
                GameManager.player.GetTargetZone().SetActive(false);
            }
            else if(currentEnemiesSpawned == 1)
            {
                GameManager.player.SetAtFightingZone(true);
            }  
        }
    }

    GameObject ChooseRandomSpawnPoint()
    {
        int index = rnd.Next(0, spawnPoints.Count);
        return spawnPoints[index];
    }
}
