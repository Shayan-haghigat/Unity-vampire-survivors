using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipWeapon : WeaponBase
{

    [SerializeField] GameObject leftWhipObject;
    [SerializeField] GameObject rightWhipObject;

    PlayerMove playerMove;
    [SerializeField] Vector2 Attack_Size = new Vector2(4f, 2f);

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }


    public override void Attack()
    {


        if (playerMove.lastHorizontalVector > 0)
        {
            rightWhipObject.SetActive(true);
            Collider2D [] colliders = Physics2D.OverlapBoxAll(rightWhipObject.transform.position, Attack_Size, 0f);
            ApplyDamage(colliders);
        }
        else{
            leftWhipObject.SetActive(true);
            Collider2D [] colliders = Physics2D.OverlapBoxAll(leftWhipObject.transform.position, Attack_Size, 0f);
            ApplyDamage(colliders);
        }
    }

    private void ApplyDamage(Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders){
            IDamageable enemy = collider.GetComponent<IDamageable>();
            if (enemy != null){
                PostDamage(weaponStatus.damage , collider.transform.position);
           // collider.GetComponent<Enemy>().TakeDamage(whipDamage);
           enemy.TakeDamage(weaponStatus.damage);
            }
        }
    }


}
