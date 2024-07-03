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
      
        StartCoroutine(AttackProcess());
    }
    IEnumerator AttackProcess()
    {
        for (int i = 0; i < weaponStatus.numberOfAttack; i++)
        {
            if (playerMove.lastHorizontalDecoupledVector> 0)
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
            yield return new WaitForSeconds(0.3f);
        }
    }




}
