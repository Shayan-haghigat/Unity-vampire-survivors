using System.Collections.Generic;
using UnityEngine;

public class PassiveItems : MonoBehaviour
{
    [SerializeField] List<Item> items;
    private Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
        if (items == null)
        {
            items = new List<Item>();
        }
    }

    public void Equip(Item itemToEquip)
    {
        if (itemToEquip == null)
        {
            Debug.LogWarning("Attempted to equip a null item.");
            return;
        }

        // Create a new instance of the item using ScriptableObject.CreateInstance
        Item newItemInstance = ScriptableObject.CreateInstance<Item>();
        newItemInstance.Init(itemToEquip.Name);
        newItemInstance.stats.Sum(itemToEquip.stats);

        items.Add(newItemInstance);
        newItemInstance.Equip(character);
    }

    public void UnEquip(Item item)
    {
        if (item == null)
        {
            Debug.LogWarning("Attempted to unequip a null item.");
            return;
        }

        if (items.Contains(item))
        {
            items.Remove(item);
            item.UnEquip(character);
        }
        else
        {
            Debug.LogWarning("Item not found in the equipped items list.");
        }
    }

    internal void UpgradeItem(UpgradeData upgradeData)
    {
        if (upgradeData == null || upgradeData.item == null)
        {
            Debug.LogWarning("Invalid upgrade data.");
            return;
        }

        Item itemToUpgrade = items.Find(id => id.Name == upgradeData.item.Name);

        if (itemToUpgrade != null)
        {
            itemToUpgrade.UnEquip(character);
            itemToUpgrade.stats.Sum(upgradeData.itemStats);
            itemToUpgrade.Equip(character);
        }
        else
        {
            Debug.LogWarning("Item to upgrade not found in the equipped items list.");
        }
    }
}