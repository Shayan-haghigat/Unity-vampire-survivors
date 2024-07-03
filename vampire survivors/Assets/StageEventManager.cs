using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEventManager : MonoBehaviour
{
    [SerializeField] StageData stageData;
     EnemiesManager enemiesManager;
    StageTime stageTime;
    int eventIndexer;

    private void Awake()
    {
        stageTime = GetComponent<StageTime>();
    }
    private void Start()
    {
        enemiesManager = FindObjectOfType<EnemiesManager>();
    }

    private void Update()
    {
        if (eventIndexer >= stageData.StageEvents.Count)
        {
            return;
        }

        if (stageTime.time > stageData.StageEvents[eventIndexer].time)
        {
            HandleStageEvent(stageData.StageEvents[eventIndexer]);
            eventIndexer++;
        }
    }

    private void HandleStageEvent(StageEvent stageEvent)
    {
        switch (stageEvent.stageEventType)
        {
            case StageEventType.SpawnEnemy:
                SpawnEnemies(stageEvent);
                break;
            case StageEventType.SpawnEnemyBoss:
                SpawnEnemyBoss(stageEvent);
                break;
        }
    }

    private void SpawnEnemies(StageEvent stageEvent)
    {
        Debug.Log(stageEvent.massage);
        for (int i = 0; i < stageEvent.Count; i++)
        {
            enemiesManager.SpawnEnemy(stageEvent.enemyToSpawn, false);
        }
    }

    private void SpawnEnemyBoss(StageEvent stageEvent)
    {
        enemiesManager.SpawnEnemy(stageEvent.enemyToSpawn, true);
    }
}