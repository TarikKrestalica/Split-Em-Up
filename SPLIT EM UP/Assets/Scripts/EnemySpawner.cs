using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;

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
        while (true)
        {
            if(curRespawnTime >= respawnTime)
            {
                Instantiate(enemy);
                curRespawnTime = 0f;
            }

            curRespawnTime += Time.deltaTime;
            yield return null;
        }
    }
}
