using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int maxHp = 1000;
    public int currentHp = 1000;

    public int armor = 0;
    public float hpRegenerationRate = 1f;
    public float hpRegenerationTime;
    public float damageBonus;
    [SerializeField] StatusBar hpBar;
    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;
    private bool isDead = false;
    [SerializeField] DataContainer _dataContainer;
   // [SerializeField] CharecterData selectedCharecter;
    private void Awake()
    {
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
    }
    private void Start()
    {
        LoadSelectedCharecter(_dataContainer.selectedCharacter);
        ApplyPersistantUpgrades();// اعمال قدرت های خریداری شده در منوی اصلی 
        hpBar.Setstatus(currentHp,maxHp);
    }
    private void LoadSelectedCharecter(CharecterData selectedCharecter)
    {
        InitAnimation(selectedCharecter.spritePrefab);
        GetComponent<WeaponManager>().AddWeapon(selectedCharecter.startingWeapon);
    }
    private void InitAnimation(GameObject spritePrefab)
    {
        GameObject animObject =Instantiate(spritePrefab,transform);
        GetComponent<Animate>().SetAnimate(animObject);

    }

    private void ApplyPersistantUpgrades()
    {
       int hpUpgradeLevel = _dataContainer.GetUpgradeLevel(PlayerPresistentUpgrades.Hp);
       maxHp += maxHp / 10 * hpUpgradeLevel;
       int damageUpgradeLevel = _dataContainer.GetUpgradeLevel(PlayerPresistentUpgrades.Damage);
       damageBonus = 1f + 0.1f * damageUpgradeLevel;
    }

    public void Update()
    {
        hpRegenerationTime += Time.deltaTime * hpRegenerationRate;
        if (hpRegenerationTime > 1f)
        {
            Heal(1);
            hpRegenerationTime -= 1f;
        }
    }
    public void TakeDamage(int damage)
    {
        if (isDead == true)
        {
            return;
        }
        ApplyArmor(ref damage);
        currentHp -= damage;
        if (currentHp <= 0)
        {
            GetComponent<CharacterGameOver>().GameOver();
            isDead = true;
        }

        hpBar.Setstatus(currentHp, maxHp);
    }

    private void ApplyArmor(ref int damage)
    {
        damage -= armor;
        if (damage < 0)
        {
            damage = 0;
        }
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
