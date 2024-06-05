using System;
using UnityEngine;

public class ThrowingDaggerWeapon : WeaponBase
{
    [SerializeField] private GameObject knifePrefab;
    
    private PlayerMove playerMove;

    private void Awake() 
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }

    // private void SpawnKnife()
    // {

    // }

    public override void Attack()// همون اسپان نایف سابق
    {
        if (knifePrefab == null || playerMove == null)
        {
            Debug.LogWarning("KnifePrefab or PlayerMove is not assigned.");
            return;
        }

        GameObject thrownKnife = Instantiate(knifePrefab);
        thrownKnife.transform.position = transform.position;

        // Assuming SetDirection takes two float arguments
        thrownKnife.GetComponent<ThrowingDaggerProjectile>().SetDirection(playerMove.lastHorizontalVector, 0f);
    }
}
