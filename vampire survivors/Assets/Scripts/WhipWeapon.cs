using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipWeapon : MonoBehaviour
{
    [SerializeField]float timetoAttack = 1.5f;
    float timer;

    [SerializeField] GameObject leftWhipObject;
    [SerializeField] GameObject rightWhipObject;

    PlayerMove playerMove;
    [SerializeField] Vector2 whipRange = new Vector2(4f, 2f);

    [SerializeField] int whipDamage = 1;

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }
    void Update()
    {
       timer -= Time.deltaTime;

       if(timer <= 0f){
        Attack();
       }
    }

    private void Attack()
    {

        timer = timetoAttack;

        if (playerMove.lastHorizontalVector > 0)
        {
            rightWhipObject.SetActive(true);
            Collider2D [] colliders = Physics2D.OverlapBoxAll(rightWhipObject.transform.position, whipRange, 0f);
            ApplyDamage(colliders);
        }
        else{
            leftWhipObject.SetActive(true);
            Collider2D [] colliders = Physics2D.OverlapBoxAll(leftWhipObject.transform.position, whipRange, 0f);
            ApplyDamage(colliders);
        }
    }

    private void ApplyDamage(Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders){
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null){
            collider.GetComponent<Enemy>().TakeDamage(whipDamage);
            }
        }
    }
}
