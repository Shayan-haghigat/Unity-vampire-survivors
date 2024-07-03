using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class CharecterData :ScriptableObject
{
    public string Name;
    public GameObject spritePrefab;
    public WeaponData startingWeapon;
}
