using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesManager : MonoBehaviour
{
    // Reference to the StageProgress script to get the current game progress.
    [SerializeField] StageProgress stageProgress;
    // Prefab for the enemy GameObject.
    [SerializeField] GameObject enemy;
    // Defines the area around the player where enemies can spawn.
    [SerializeField] Vector2 spawnArea;

    // Reference to the player GameObject.
    GameObject player;
    // List to keep track of active boss enemies.
    List<Enemy> bossEnemiesList;
    // Current combined health of all active boss enemies.
    int currentBossHealth;
    // UI Slider to display the boss health bar.
    [SerializeField] Slider bossHealthBar;
    // Flag to check if an enemy is already spawned (seems to be intended to limit to one enemy at a time, but the logic is commented out).
    bool enemySpawned = false;

    private void Start()
    {
        player = GameManager.Instance.playerTransform.gameObject; // Get the player GameObject from the GameManager.
        // Find and assign the boss health bar UI element.
        bossHealthBar = FindObjectOfType<BossHpBar>(true).GetComponent<Slider>();
        // Find and assign the StageProgress component.
        stageProgress = FindObjectOfType<StageProgress>();
    }

    private void Update()
    {
        UpdateBossHealth(); // Update the boss health bar every frame.
    }

    // Updates the boss health bar based on the current health of all boss enemies.
    private void UpdateBossHealth()
    {
        if (bossEnemiesList == null) {
            if (bossHealthBar.gameObject.activeSelf) {
                bossHealthBar.gameObject.SetActive(false);
            }
            return;
        }

        int currentCombinedHp = 0;
        int newMaxValueForBossBar = 0;

        for (int i = bossEnemiesList.Count - 1; i >= 0; i--)
        {
            Enemy currentBoss = bossEnemiesList[i];
            if (currentBoss == null) // Enemy was destroyed
            {
                bossEnemiesList.RemoveAt(i);
            }
            else
            {
                currentCombinedHp += currentBoss.stats.hp;
                // Assuming initialHpForBossBar was set correctly during spawn
                newMaxValueForBossBar += currentBoss.initialHpForBossBar;
            }
        }

        if (bossEnemiesList.Count == 0)
        {
            if (bossHealthBar.gameObject.activeSelf)
            {
                bossHealthBar.gameObject.SetActive(false);
            }
        }
        else
        {
            if (!bossHealthBar.gameObject.activeSelf)
            {
                bossHealthBar.gameObject.SetActive(true);
            }
            bossHealthBar.maxValue = newMaxValueForBossBar;
            bossHealthBar.value = currentCombinedHp;
        }
    }

    // Spawns a new enemy.
    public void SpawnEnemy(EnemyData enemyToSpawn, bool isBoss)
    {
      //  if (enemySpawned) // This commented-out block would limit spawning to one enemy at a time.
       // {
       //     Debug.Log("Enemy already spawned. No new enemy will be spawned.");
       //     return;
       // }

        // Generate a random position around the player to spawn the enemy.
        Vector3 position = GenerateRandomPosition();
        position += player.transform.position;

        // Instantiate the main enemy GameObject.
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = position;

        // Get the Enemy component and configure it.
        Enemy newEnemyComponent = newEnemy.GetComponent<Enemy>();
        newEnemyComponent.SetTarget(player); // Set the player as the target.
        newEnemyComponent.setStats(enemyToSpawn.stats); // Apply base stats from EnemyData.
        newEnemyComponent.initialHpForBossBar = enemyToSpawn.stats.hp; // Store initial HP for boss bar before progress scaling
        newEnemyComponent.UpdateStatsForProgress(stageProgress.Progress); // Scale stats based on game progress.

        if (isBoss)
        {
            SpawnBossEnemy(newEnemyComponent); // If it's a boss, add it to the boss list.
        }
        newEnemy.transform.parent = transform; // Set this manager as the parent of the new enemy.
        
        // Instantiate the enemy's visual representation (sprite/animation).
        GameObject spriteObject = Instantiate(enemyToSpawn.animatedPrefab);
        spriteObject.transform.parent = newEnemy.transform; // Make the sprite a child of the enemy logic object.
        spriteObject.transform.localPosition = Vector3.zero; // Reset local position.

        enemySpawned = true; // Set flag (though its current use is limited by the commented-out block above).
    }

    // Adds a new boss to the tracking list and updates the boss health bar.
    private void SpawnBossEnemy(Enemy newBoss)
    {
        if (bossEnemiesList == null)
        {
            bossEnemiesList = new List<Enemy>();
        }
        bossEnemiesList.Add(newBoss);
        // UpdateBossHealth() in the next Update() call will handle bar visibility and values.
    }

    // Generates a random position within the defined spawn area, offset from the player.
    private Vector3 GenerateRandomPosition()
    {
        float f = UnityEngine.Random.value > 0.5f ? -1f : 1f; // Randomly choose a positive or negative direction.
        Vector3 position = new Vector3();

        // Randomly decide whether to spawn on the X-axis or Y-axis edge of the spawn area.
        if (UnityEngine.Random.value > 0.5f)
        {
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x); // Random X within bounds.
            position.y = spawnArea.y * f; // Y at the edge of the spawn area.
        }
        else
        {
            position.y = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y); // Random Y within bounds.
            position.x = spawnArea.x * f; // X at the edge of the spawn area.
        }

        position.z = 0f; // Ensure Z is 0 for 2D.
        return position;
    }
}
