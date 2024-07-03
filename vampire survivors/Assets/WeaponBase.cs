using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{   
    public WeaponStats weaponStatus;
    public WeaponData weaponData;
    public float timer;
    Character wielder;
    public void Update() {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            Attack();
            timer = weaponStatus.timeToAttack;
        }
    }

    public virtual void SetData(WeaponData wd){
        weaponData = wd;
       
        weaponStatus = new WeaponStats(wd.weaponStatus.damage,wd.weaponStatus.timeToAttack,wd.weaponStatus.numberOfAttack);
    }

    public abstract void Attack();

    public virtual void PostDamage(int damage , Vector3 targetPostion)
    {
        MessageSystem.instance.PostMassage(damage.ToString() , targetPostion);//اطلاعات دمیج وارد شده توسط سلاح به نمایش در میاد 
    }
    public void Upgrade(UpgradeData upgradeData)
    {
        weaponStatus.Sum(upgradeData.weaponUpgradesStats); 
    }

    public void AddOwnerCharacter(Character character)
    {
        wielder = character;
    }

    public int GetDamage()
    {
        int damage = (int)(weaponData.weaponStatus.damage * wielder.damageBonus);
        return damage;
    }
}
