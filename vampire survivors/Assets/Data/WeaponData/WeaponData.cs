using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class WeaponStats
{
    public int damage;
    public float timeToAttack ;
    public float numberOfAttack ;
    

    public WeaponStats(int damage , float timeToAttack, float numberOfAttack)
    {
        this.damage = damage;
        this.timeToAttack = timeToAttack;
        this.numberOfAttack = numberOfAttack;
    }
    internal void Sum(WeaponStats weaponUpgradeStats)
    {
        this.damage+= weaponUpgradeStats.damage;
        this.timeToAttack+= weaponUpgradeStats.timeToAttack;
        this.numberOfAttack+= weaponUpgradeStats.numberOfAttack;
    }
}

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public string Name ;
    public WeaponStats weaponStatus;
    public GameObject weaponBasePrefab;
    public List<UpgradeData> upgrades;
}
