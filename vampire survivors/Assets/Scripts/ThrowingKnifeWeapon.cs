using System;
using UnityEngine;

public class ThrowingKnifeWeapon : WeaponBase
{
    [SerializeField] GameObject knifePrefab;
    [SerializeField] float spread = 0.5f;

    public override void Attack()
    {
        UpdateVectorOfAttack();
        if (knifePrefab == null)
        {
            Debug.LogWarning("KnifePrefab is not assigned.");
            return;
        }

        if (weaponStatus == null)
        {
            Debug.LogWarning("WeaponStatus is not assigned or initialized.");
            return;
        }

        for (int i = 0; i < weaponStatus.numberOfAttack; i++)
        {
            Vector3 newKnifePosition = transform.position;
            newKnifePosition.y -= (spread * weaponStatus.numberOfAttack) / 2;
            newKnifePosition.y += i * spread;

            GameObject thrownKnife = Instantiate(knifePrefab, newKnifePosition, Quaternion.identity);
            if (thrownKnife == null)
            {
                Debug.LogWarning("Thrown knife instantiation failed.");
                return;
            }

            ThrowingKnifeProjectile throwingKnifeProjectile = thrownKnife.GetComponent<ThrowingKnifeProjectile>();
            if (throwingKnifeProjectile != null)
            {
                throwingKnifeProjectile.SetDirection(_vectorOfAttack.x, _vectorOfAttack.y);
                throwingKnifeProjectile.damage = GetDamage();
            }
            else
            {
                Debug.LogWarning("Thrown knife does not have a ThrowingKnifeProjectile component.");
            }
        }
    }
}