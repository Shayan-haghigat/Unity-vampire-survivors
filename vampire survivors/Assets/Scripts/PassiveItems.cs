using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItems : MonoBehaviour
{
    [SerializeField] List<Item> items;
    Character character;

    [SerializeField] Item armorTest;

    private void Awake() {
        character = GetComponent<Character>();
    }

    private void Start() {
        Equip(armorTest);
    }
    
    public void Equip(Item item){
        if (items == null) {
            items = new List<Item>(); 
        }
        items.Add(item);
        item.Equip(character);
    }

    public void UnEquip(Item item){
        if (items != null && items.Contains(item)) {
            items.Remove(item);
            item.UnEquip(character);
        }
    }
}