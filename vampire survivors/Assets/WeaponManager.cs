using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponObjectContainer;
    [SerializeField] WeaponData StartingWeapon;

    private void Start() {
        if (StartingWeapon != null) {
            AddWeapon(StartingWeapon);
        } else {
            Debug.LogError("StartingWeapon is not assigned in the inspector.");
        }
    }

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

        GameObject gameObject = Instantiate(weaponData.weaponBasePrefab, weaponObjectContainer);
        WeaponBase weaponBase = gameObject.GetComponent<WeaponBase>();

        if (weaponBase != null) {
            weaponBase.SetData(weaponData);
        } else {
            Debug.LogError("The instantiated weapon prefab does not have a WeaponBase component.");
        }
    }
}
