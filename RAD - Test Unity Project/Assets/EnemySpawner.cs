using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRate = 1f;
    public float spawnTimer = 0f;
    public GameObject enemy;
    public float spawnRangeY;
    public float spawnRangeX;

    public void Update()
    {
        SpawnTimer();
    }

    public void Spawn()
    {

        float direction = UnityEngine.Random.value;
        float spawnX = direction > 0.5f ? spawnRangeX : -spawnRangeX;
        enemy.GetComponent<EnemyMovement>().moveRight = direction < 0.5f;
        Vector3 spawnPoint = new Vector3(transform.position.x + spawnX, transform.position.y + UnityEngine.Random.Range(-spawnRangeY, spawnRangeY), transform.position.z);
        Instantiate(enemy, spawnPoint, Quaternion.identity);
    }

    public void SpawnTimer()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            Spawn();
            spawnTimer = 0f;
        }
    }
}
