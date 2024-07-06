using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperHealthPickUP : MonoBehaviour, IPickUpObject
{
    [SerializeField] int count;
    [SerializeField] int amount;
    [SerializeField] int healAmount;
    public void OnPickUp(Character character)
    {
        character.coins.Add(count);
        character.level.AddExperience(amount);
        character.Heal(healAmount);
    }
}
