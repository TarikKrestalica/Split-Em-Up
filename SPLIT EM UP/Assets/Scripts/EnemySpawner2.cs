using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner2 : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] private List<GameObject> spawnPoints;
    private System.Random rnd = new System.Random();

    [Range(0, 1)]
    [SerializeField] private int numOfEnemies;
    private int currentEnemiesSpawned = 0;

    [Header("Spawning System")]
    [Range(0, 3f)]
    [SerializeField] private float respawnTime;
    private float curRespawnTime = 0f;

    private int enemiesDefeated = 0;

    private void Start()
    {
        curRespawnTime = respawnTime;
    }

    private void Update()
    {
        if (GameManager.player.GetEnemiesDefeated() == numOfEnemies)
        {
            GameManager.player.SetAtFightingZone(false);
            GameManager.player.ResetEnemiesDefeated();
            GameManager.player.GetTargetZone().SetActive(false);
            return;
        }

        if (currentEnemiesSpawned >= numOfEnemies)
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

            if (currentEnemiesSpawned == 1)
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

    public void AddEnemyDefeated()
    {
        enemiesDefeated += 1;
    }

    public int GetEnemiesDefeated()
    {
        return enemiesDefeated;
    }

    public int GetNumberOfEnemies()
    {
        return numOfEnemies;
    }

}
