using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageEventType
{
    SpawnEnemy,
    SpawnEnemyBoss
}
[Serializable]
public class StageEvent
{
    public float time;
    public StageEventType stageEventType;
    public string massage;
    public EnemyData enemyToSpawn;
    public int Count;

}
[CreateAssetMenu]
public class StageData : ScriptableObject
{
    public List<StageEvent> StageEvents;
    
}
