using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{   
    public WeaponStats weaponStatus;
    public WeaponData weaponData;
    public float TimetoAttack = 1f ;
    public float timer;
    public void Update() {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            Attack();
            timer = TimetoAttack;
        }
    }

    public virtual void SetData(WeaponData weaponData){
        this.weaponData = weaponData;
        TimetoAttack = weaponData.weaponStatus.timeToAttack;
        weaponStatus = new WeaponStats(weaponData.weaponStatus.damage,weaponData.weaponStatus.timeToAttack);
    }

    public abstract void Attack();

    public virtual void PostDamage(int damage , Vector3 targetPostion)
    {
        MessageSystem.instance.PostMassage(damage.ToString() , targetPostion);//اطلاعات دمیج وارد شده توسط سلاح به نمایش در میاد 
    }
}
