using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ItemStats
{
    public int armor;

    internal void Sum(ItemStats stats)
    {
        armor += stats.armor;
    }
}

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string Name ;
    public ItemStats stats ;
    public List<UpgradeData> upgrades;

    public void Init(string Name)
    {
        this.Name = Name;
        stats = new ItemStats();
        upgrades = new List<UpgradeData>();
    }
    public void Equip(Character character)
    {
        character.armor += stats.armor;
    }
    public void UnEquip(Character character)
    {
        character.armor -= stats.armor;
    }

    //از این به بعد میشه مثل فایل ها ایتم های جدید هم ایجاد کرد 
}
