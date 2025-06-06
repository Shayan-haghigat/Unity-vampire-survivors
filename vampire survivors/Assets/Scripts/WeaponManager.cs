using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Container for instantiated weapon objects.
    [SerializeField] Transform weaponObjectContainer;
    //[SerializeField] WeaponData StartingWeapon;
    // List to store all active weapon instances.
    List<WeaponBase> weapons;
    // Reference to the Character component that owns these weapons.
    Character _character;
    private void Awake()
    {
        weapons = new List<WeaponBase>();
        _character = GetComponent<Character>();
    }

   // private void Start() {
     //   if (StartingWeapon != null) {
    //        AddWeapon(StartingWeapon);
    //    } else {
    //        Debug.LogError("StartingWeapon is not assigned in the inspector.");
    //    }
   // }

    // Adds a new weapon to the player.
    public void AddWeapon(WeaponData weaponData){
        if (weaponData == null) {
            Debug.LogError("WeaponData provided to AddWeapon is null.");
            return;
        }

        if (weaponData.weaponBasePrefab == null) {
            Debug.LogError("WeaponData's weaponBasePrefab is null. Make sure it is assigned.");
            return;
        }

        if (weaponObjectContainer == null) {
            Debug.LogError("weaponObjectContainer is not assigned.");
            return;
        }

        // Instantiates the weapon prefab and sets it up.
        GameObject gameObject = Instantiate(weaponData.weaponBasePrefab, weaponObjectContainer);
        WeaponBase weaponBase = gameObject.GetComponent<WeaponBase>();
        weaponBase.SetData(weaponData); // Assigns weapon-specific data.
        weapons.Add(weaponBase); // Adds the weapon to the list of active weapons.
        weaponBase.AddOwnerCharacter(_character); // Sets the owner of the weapon.
        Level level = GetComponent<Level>(); // Gets the Level component of the player.
        if(level != null)
        {
            // Adds the weapon's possible upgrades to the list of available upgrades when the player levels up.
            level.AddUpgradesIntoTheListOfAvailableUpgrades(weaponData.upgrades);
        }
             
        if (weaponBase != null) {
            weaponBase.SetData(weaponData);

        } else {
            Debug.LogError("The instantiated weapon prefab does not have a WeaponBase component.");
        }
    }
    // Upgrades an existing weapon.
    internal void UpgradeWeapon(UpgradeData upgradeData)
    {
        // Finds the weapon to upgrade from the list of active weapons.
        WeaponBase weaponToUpgrade = weapons.Find(wd => wd.weaponData==upgradeData.weaponData);
        // Applies the upgrade to the weapon.
        weaponToUpgrade.Upgrade(upgradeData);
    }
}
