using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] StageProgress stageProgress;
    [SerializeField] GameObject enemy;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] float spawntimer;
    GameObject player;
    private void Start()
    {
        player = GameManager.Instance.playerTransform.gameObject;
    }
    

    public void SpawnEnemy(EnemyData enemyToSpawn)
    {
        Vector3 position = GenerateRandomPosition();
        position += player.transform.position;
        // spawning main enemy object 
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = position;
        Enemy newEnemyComponent = newEnemy.GetComponent<Enemy>();
        newEnemyComponent.SetTarget(player);
        newEnemyComponent.setStats(enemyToSpawn.stats);
        newEnemyComponent.UpdateStatsForProgress(stageProgress.Progress);
        newEnemy.transform.parent = transform;
        
        // spawning sprite object of the enemy

        GameObject spriteObject = Instantiate(enemyToSpawn.animatedPrefab);
        spriteObject.transform.parent = newEnemy.transform;
        spriteObject.transform.localPosition = Vector3.zero;

    }

    private Vector3 GenerateRandomPosition()
    {
        float f = UnityEngine.Random.value > 0.5f ? -1f : 1f;
        Vector3 position = new Vector3();
        if (UnityEngine.Random.value > 0.5f)
        {
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * f;
        }
        else
        {
           position.y = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y); 
            position.x = spawnArea.x * f;
        }

        position.z = 0f;
        return position;


    }
}
