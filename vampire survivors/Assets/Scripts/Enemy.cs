using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class EnemyStats
{
    // Enemy's health points.
    public int hp = 4;
    // Damage dealt by the enemy.
    public int damage = 1;
    // Experience points awarded to the player upon defeating the enemy.
    public int experience_reward = 400;
    // Enemy's movement speed.
    public float movespeed = 1f ;

    public EnemyStats(EnemyStats stats)
    {
        this.hp = stats.hp;
        this.damage = stats.damage;
        this.experience_reward = stats.experience_reward;
        this.movespeed = stats.movespeed;
    }

    // Applies a progress multiplier to enemy stats, making them stronger over time.
    public void ApplyProgress(float progress)
    {
        this.hp = (int)(hp * progress);
        this.damage = (int)(damage * progress);
    }
}

public class Enemy : MonoBehaviour,IDamageable
{
   Transform targetDistination; // Target destination for movement (usually the player).
    
    GameObject targetgameObject; // Reference to the target GameObject (player).
    Character targetCharacter; // Reference to the Character component of the target.
    Rigidbody2D rigidbody2D; // Rigidbody2D component for physics-based movement.
    public EnemyStats stats; // Current stats of the enemy.
    public int initialHpForBossBar; // Initial HP for boss bar calculation, set before progress scaling.
    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    // Sets the target for the enemy to follow and attack.
    public void SetTarget(GameObject target){
        targetgameObject = target;
        targetDistination = target.transform;
    }

    private void FixedUpdate() {
        // Moves the enemy towards the target destination.
        rigidbody2D.velocity = (targetDistination.position - transform.position).normalized * stats.movespeed;
        // برای تغییرات سختی بازی میشه سرعت دشمن رو با زمان تغییر داد برای اخر کار مد نظر باشه
    }

    // Called when the enemy collides with another object.
    private void OnCollisionStay2D(Collision2D other) {
        // If the collision is with the target (player), attack.
        if (other.gameObject == targetgameObject) {
            Attack();
        }
    }
    // Attacks the target character.
    private void Attack(){
        if (targetCharacter == null)
        {
            targetCharacter = targetgameObject.GetComponent<Character>();
        }

        targetCharacter.TakeDamage(stats.damage); // Deals damage to the target character.
    }

    // Called when the enemy takes damage.
    public void TakeDamage(int damage){
        stats.hp -= damage;
        if(stats.hp <= 0){
            // If health drops to 0 or below, award experience to the player, check for item drops, and destroy the enemy.
            targetgameObject.GetComponent<Level>().AddExperience(stats.experience_reward);
            GetComponent<DropOnDestroy>().CheckDrop();
            Destroy(gameObject);
        }
    }

    // Sets the initial stats for the enemy.
    public void setStats(EnemyStats stats)
    {
        this.stats = new EnemyStats(stats);
    }

    // Updates enemy stats based on game progress (e.g., time survived).
    public void UpdateStatsForProgress(float progress)
    {
        stats.ApplyProgress(progress);
    }
}
