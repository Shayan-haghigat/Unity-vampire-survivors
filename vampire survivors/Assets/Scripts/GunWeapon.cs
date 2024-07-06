using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : WeaponBase
{
    [SerializeField] GameObject bulletPrefab; // Change from knifePrefab to bulletPrefab
    [SerializeField] float spread = 0.5f; // Adjust as necessary for bullet spread

    public override void Attack()
    {
        UpdateVectorOfAttack();
        for (int i = 0; i < weaponStatus.numberOfAttack; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            Vector3 newBulletPostion = transform.position;
            bullet.transform.position = newBulletPostion;
            ThrowingKnifeProjectile throwingDaggerProjectile = bullet.GetComponent<ThrowingKnifeProjectile>();
            throwingDaggerProjectile.SetDirection(_vectorOfAttack.x , _vectorOfAttack.y);
            throwingDaggerProjectile.damage = GetDamage();
        }
    }
} 