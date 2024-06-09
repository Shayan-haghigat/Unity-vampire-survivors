/*using System;
using UnityEngine;

public class ThrowingKnifeWeapon : WeaponBase
{
     PlayerMove playerMove;
    [SerializeField] GameObject knifePrefab;
    

    private void Awake() 
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }

    // private void SpawnKnife()
    // {

    // }

    public override void Attack()// همون اسپان نایف سابق
    {
        if (knifePrefab == null )
        {
            Debug.LogWarning("KnifePrefab or PlayerMove is not assigned.");
            return;
        }

        GameObject thrownKnife = Instantiate(knifePrefab);
        thrownKnife.transform.position = transform.position;
        ThrowingKnifeProjectile throwingDaggerProjectile = thrownKnife.GetComponent<ThrowingKnifeProjectile>();
        // Assuming SetDirection takes two float arguments
       throwingDaggerProjectile.SetDirection(playerMove.lastHorizontalVector, 0f);
        throwingDaggerProjectile.damage = weaponStatus.damage;
    }
}
*/
using System;
using UnityEngine;

public class ThrowingKnifeWeapon : WeaponBase
{
    PlayerMove playerMove;
    [SerializeField] GameObject knifePrefab;
    [SerializeField] float spread = 0.5f;

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
        if (playerMove == null)
        {
            Debug.LogWarning("PlayerMove component is not found in the parent.");
        }
    }

    public override void Attack()
    {
        if (knifePrefab == null)
        {
            Debug.LogWarning("KnifePrefab is not assigned.");
            return;
        }

        if (playerMove == null)
        {
            Debug.LogWarning("PlayerMove component is not assigned.");
            return;
        }

        // Verify weaponStatus is not null
        if (weaponStatus == null)
        {
            Debug.LogWarning("WeaponStatus is not assigned or initialized.");
            return;
        }

        for(int i =0; i<weaponStatus.numberOfAttack; i++)
        {
                 GameObject thrownKnife = Instantiate(knifePrefab, transform.position, Quaternion.identity);
                if (thrownKnife == null)
             {
            Debug.LogWarning("Thrown knife instantiation failed.");
            return;
             }
                Vector3 newKnifePosition = transform.position;
                newKnifePosition.y -= (spread*weaponStatus.numberOfAttack)/2;
            newKnifePosition.y += i * spread;
            thrownKnife.transform.position = newKnifePosition;

        ThrowingKnifeProjectile throwingDaggerProjectile = thrownKnife.GetComponent<ThrowingKnifeProjectile>();
        if (throwingDaggerProjectile != null)
        {
            // Assuming SetDirection takes two float arguments: horizontal and vertical
            throwingDaggerProjectile.SetDirection(playerMove.lastHorizontalVector, 0f);
            // Assuming weaponStatus is defined in the base class or somewhere else
            throwingDaggerProjectile.damage = weaponStatus.damage;
        }
        else
        {
            Debug.LogWarning("Thrown knife does not have a ThrowingKnifeProjectile component.");
        }
        }
    }
}
