using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DirectionOfAttack
{
    None,
    Forward,
    LeftRight,
    UpDown
}

public abstract class WeaponBase : MonoBehaviour
{
    protected PlayerMove _playerMove;
    public WeaponStats weaponStatus;
    public WeaponData weaponData;
    public float timer;
    protected Character wielder;
    public Vector2 _vectorOfAttack;
    [SerializeField] DirectionOfAttack _attackDirection;

    private void Awake()
    {
        _playerMove = GetComponentInParent<PlayerMove>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            Attack();
            timer = weaponStatus.timeToAttack;
        }
    }

    public virtual void SetData(WeaponData wd)
    {
        weaponData = wd;
        weaponStatus = new WeaponStats(wd.weaponStatus.damage, wd.weaponStatus.timeToAttack, wd.weaponStatus.numberOfAttack);
    }

    public abstract void Attack();

    public virtual void PostDamage(int damage, Vector3 targetPosition)
    {
        MessageSystem.instance.PostMassage(damage.ToString(), targetPosition); // Display the damage information
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

    public void ApplyDamage(Collider2D[] colliders)
    {
        int damage = GetDamage();
        foreach (Collider2D collider in colliders)
        {
            IDamageable enemy = collider.GetComponent<IDamageable>();
            if (enemy != null)
            {
                PostDamage(damage, collider.transform.position);
                enemy.TakeDamage(damage);
            }
        }
    }

    public void UpdateVectorOfAttack()
    {
        if (_attackDirection == DirectionOfAttack.None)
        {
            _vectorOfAttack = Vector2.zero;
            return;
        }

        switch (_attackDirection)
        {
            case DirectionOfAttack.Forward:
                _vectorOfAttack.x = _playerMove.lastHorizontalcoupleVector;
                _vectorOfAttack.y = _playerMove.lastVerticalcoupleVector;// Assuming GetDirection() provides the direction vector based on player movement
                break;
            case DirectionOfAttack.LeftRight:
                _vectorOfAttack.x = _playerMove.lastHorizontalDecoupledVector;
                _vectorOfAttack.y = 0f;
                break;
            case DirectionOfAttack.UpDown:
                _vectorOfAttack.x = 0f;
                _vectorOfAttack.y = _playerMove.lastVerticalDecoupledVector;
                break;
        }

        _vectorOfAttack = _vectorOfAttack.normalized;
    }
}
