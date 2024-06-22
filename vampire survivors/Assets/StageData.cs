using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class StageEvent
{
    public float time;
    public string massage;
    public Enemy enemyToSpawn;
    public int Count;

}
[CreateAssetMenu]
public class StageData : ScriptableObject
{
    public List<StageEvent> StageEvents;
    
}
