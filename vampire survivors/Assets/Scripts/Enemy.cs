using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class EnemyStats
{
    public int hp = 4;
    public int damage = 1;
    public int experience_reward = 400;
    public float movespeed = 1f ;

    public EnemyStats(EnemyStats stats)
    {
        this.hp = stats.hp;
        this.damage = stats.damage;
        this.experience_reward = stats.experience_reward;
        this.movespeed = stats.movespeed;
    }

    public void ApplyProgress(float progress)
    {
        this.hp = (int)(hp * progress);
        this.damage = (int)(damage * progress);
    }
}

public class Enemy : MonoBehaviour,IDamageable
{
   Transform targetDistination;
    
    GameObject targetgameObject;
    Character targetCharacter;
    Rigidbody2D rigidbody2D;
    public EnemyStats stats;
    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    public void SetTarget(GameObject target){
        targetgameObject = target;
        targetDistination = target.transform;
    }

    private void FixedUpdate() {
        rigidbody2D.velocity = (targetDistination.position - transform.position).normalized * stats.movespeed;
        // برای تغییرات سختی بازی میشه سرعت دشمن رو با زمان تغییر داد برای اخر کار مد نظر باشه
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject == targetgameObject) {
            Attack();
        }
    }
    private void Attack(){
        if (targetCharacter == null)
        {
            targetCharacter = targetgameObject.GetComponent<Character>();
        }

        targetCharacter.TakeDamage(stats.damage);
    }

    public void TakeDamage(int damage){
        stats.hp -= damage;
        if(stats.hp <= 0){
            targetgameObject.GetComponent<Level>().AddExperience(stats.experience_reward);
            GetComponent<DropOnDestroy>().CheckDrop();
            Destroy(gameObject);
        }
    }

    public void setStats(EnemyStats stats)
    {
        this.stats = new EnemyStats(stats);
    }

    public void UpdateStatsForProgress(float progress)
    {
        stats.ApplyProgress(progress);
    }
}
