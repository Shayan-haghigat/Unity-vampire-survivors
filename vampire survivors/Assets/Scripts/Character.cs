using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int maxHp = 1000;
    public int currentHp = 1000;
    [SerializeField] StatusBar hpBar;
    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;

    private void Awake()
    {
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
    }
    private void Start()
    {
        hpBar.Setstatus(currentHp,maxHp);
    }
    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
           // Destroy(gameObject);
           Debug.Log("Character died");
        }

        hpBar.Setstatus(currentHp, maxHp);
    }

    public void Heal (int heal)
    {
        if (currentHp <= 0)
        {
            return;
        }

        currentHp += heal;
        if (currentHp > maxHp){
            currentHp = maxHp;
        }
        hpBar.Setstatus(currentHp, maxHp);
    }

}
