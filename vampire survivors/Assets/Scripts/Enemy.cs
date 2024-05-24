using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   Transform targetDistination;
    [SerializeField] float speed;
    GameObject targetgameObject;
    Character targetCharacter;

    Rigidbody2D rigidbody2D;



    [SerializeField] int hp = 4;
    [SerializeField] int damage = 1;
    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    public void SetTarget(GameObject target){
        targetgameObject = target;
        targetDistination = target.transform;
    }

    private void FixedUpdate() {
        rigidbody2D.velocity = (targetDistination.position - transform.position).normalized * speed;
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

        targetCharacter.TakeDamage(damage);
    }

    public void TakeDamage(int damage){
        hp -= damage;
        if(hp <= 0){
            Destroy(gameObject);
        }
    }

}
