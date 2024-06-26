using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEventManager : MonoBehaviour
{
    [SerializeField] StageData stageData;
    [SerializeField] EnemiesManager enemiesManager;
    StageTime stageTime;
    int eventIndexer;

    private void Awake()
    {
        stageTime = GetComponent<StageTime>();
    }

    private void Update()
    {
        if (eventIndexer >= stageData.StageEvents.Count)
        {
            for (int i = 0; i < 25; i++)
            {
                enemiesManager.SpawnEnemy(stageData.StageEvents[eventIndexer].enemyToSpawn);
            }
            return;
        }
        if (stageTime.time > stageData.StageEvents[eventIndexer].time)
        {
            Debug.Log(stageData.StageEvents[eventIndexer].massage);
            for (int i = 0; i < stageData.StageEvents[eventIndexer].Count; i++)
            {
                enemiesManager.SpawnEnemy(stageData.StageEvents[eventIndexer].enemyToSpawn);
            }
            eventIndexer++;
        }
    }
}
