using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerUpgradeUIElement : MonoBehaviour
{
    [SerializeField] PlayerPresistentUpgrades _upgrades;
    [SerializeField] TextMeshProUGUI upgradeName;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI price;

    [SerializeField]  DataContainer _dataContainer;

    private void Start()
    {
        UpdateElement();
    }

    public void Upgrade()
    {
        PlayerUpgrades playerUpgrades = _dataContainer.upgrades[(int)_upgrades];
        if (playerUpgrades.level >= playerUpgrades.max_level)
        {
           return; 
        }
        if (_dataContainer.coins >= playerUpgrades.costToUpgrade)
        {
            _dataContainer.coins -= playerUpgrades.costToUpgrade;
            playerUpgrades.level += 1;
            UpdateElement();
        }
    }
    void UpdateElement()
    {
        PlayerUpgrades playerUpgrades = _dataContainer.upgrades[(int)_upgrades];
        upgradeName.text = _upgrades.ToString();
        level.text = playerUpgrades.level.ToString();
        price.text = playerUpgrades.costToUpgrade.ToString();
    }
}
