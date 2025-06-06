using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Player's maximum health points.
    public int maxHp = 1000;
    // Player's current health points.
    public int currentHp = 1000;

    // Player's armor, reduces incoming damage.
    public int armor = 0;
    // Rate at which health regenerates per second.
    public float hpRegenerationRate = 1f;
    // Timer for health regeneration.
    public float hpRegenerationTime;
    // Bonus to damage dealt by the player.
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
    // Loads the selected character's data, including their sprite and starting weapon.
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
    // Applies persistent upgrades purchased by the player in the main menu.
    // These upgrades can include increased health, damage, etc.
    private void ApplyPersistantUpgrades()
    {
       int hpUpgradeLevel = _dataContainer.GetUpgradeLevel(PlayerPresistentUpgrades.Hp);
       maxHp += maxHp / 10 * hpUpgradeLevel;
       int damageUpgradeLevel = _dataContainer.GetUpgradeLevel(PlayerPresistentUpgrades.Damage);
       damageBonus = 1f + 0.1f * damageUpgradeLevel;
    }

    public void Update()
    {
        // Regenerates health over time.
        hpRegenerationTime += Time.deltaTime * hpRegenerationRate;
        while (hpRegenerationTime >= 1f) // Changed from if to while and condition to >=
        {
            Heal(1); // Heal 1 HP per second (based on hpRegenerationRate).
            hpRegenerationTime -= 1f;
        }
    }
    // Called when the player takes damage.
    public void TakeDamage(int damage)
    {
        if (isDead == true)
        {
            return;
        }
        ApplyArmor(ref damage); // Apply armor to reduce damage.
        currentHp -= damage;
        if (currentHp <= 0)
        {
            GetComponent<CharacterGameOver>().GameOver(); // Trigger game over if health drops to 0 or below.
            isDead = true;
        }

        hpBar.Setstatus(currentHp, maxHp); // Update the health bar UI.
    }

    // Applies armor to reduce incoming damage.
    private void ApplyArmor(ref int damage)
    {
        damage -= armor;
        if (damage < 0) // Damage cannot be negative.
        {
            damage = 0;
        }
    }
    // Called to heal the player.
    public void Heal (int heal)
    {
        if (currentHp <= 0) // Cannot heal if already dead.
        {
            return;
        }

        currentHp += heal;
        if (currentHp > maxHp){ // Health cannot exceed maximum health.
            currentHp = maxHp;
        }
        hpBar.Setstatus(currentHp, maxHp); // Update the health bar UI.
    }

}
