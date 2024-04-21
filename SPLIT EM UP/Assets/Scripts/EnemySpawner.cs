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

    private void Update()
    {
        if(currentEnemiesSpawned >= numOfEnemies)
        {
            return;
        }

        if(curRespawnTime > 0f)
        {
            curRespawnTime -= Time.deltaTime;
        }
        else
        {
            GameObject spawnPoint = ChooseRandomSpawnPoint();
            Instantiate(enemy, spawnPoint.transform.position, Quaternion.identity);
            ++currentEnemiesSpawned;

            if (currentEnemiesSpawned == numOfEnemies)  // Won their fighting zone, move to next stages
            {
                GameManager.player.SetAtFightingZone(false);
                GameManager.player.GetTargetZone().SetActive(false);
            }
            else if (currentEnemiesSpawned == 1)
            {
                GameManager.player.SetAtFightingZone(true);
            }

            curRespawnTime = respawnTime;
        }
        
    }

    GameObject ChooseRandomSpawnPoint()
    {
        int index = rnd.Next(0, spawnPoints.Count);
        return spawnPoints[index];
    }


}
