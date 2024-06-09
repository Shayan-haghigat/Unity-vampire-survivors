using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    int experience = 0;
    int level = 1;
    [SerializeField] ExperienceBar experienceBar;
    [SerializeField] UpgradePanelManager upgradePanel;
    [SerializeField] List<UpgradeData> upgrades;
    List<UpgradeData> selectedUpgrades;
    [SerializeField]List<UpgradeData> acquiredUpgrades;
    WeaponManager weaponManager;
    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
    }
    int TO_LEVEL_UP
    {
        get
        {
            return level * 1000;
        }
    }
    internal void AddUpgradesIntoTheListOfAvailableUpgrades(List<UpgradeData> UpgradesToAdd)
    {
        this.upgrades.AddRange(UpgradesToAdd);
    }
    private void Start()
    {
        experienceBar.UpdateExperienceSlider(experience,TO_LEVEL_UP);
        experienceBar.SetLevelText(level);
    }
    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUP();
        experienceBar.UpdateExperienceSlider(experience,TO_LEVEL_UP);
    }
    public void CheckLevelUP()
    {
        if (experience >=TO_LEVEL_UP) {
           LevelUp();
        }
    }
    private void LevelUp()
    {
        if (selectedUpgrades == null)
        {
            selectedUpgrades = new List<UpgradeData>();
        }
        selectedUpgrades.Clear();
        selectedUpgrades.AddRange(GetUpgrades(4));
        upgradePanel.OpenPanel(selectedUpgrades);
        experience -= TO_LEVEL_UP;
        level += 1;
        experienceBar.SetLevelText(level);
    }

    public List<UpgradeData> GetUpgrades(int count)
    {
        List<UpgradeData> upgrade_list = new List<UpgradeData>() ;

        if ( count > upgrades.Count)
        {
            count = upgrades.Count ;
        }
        for (int i = 0; i < count; i++)
        {
            upgrade_list.Add(upgrades[Random.Range(0 , upgrades.Count)]);
        }



        return upgrade_list;
    }

    public void Upgrade(int selectedUpgradeID)
    {
        UpgradeData upgradeData = selectedUpgrades[selectedUpgradeID];

        if (acquiredUpgrades == null)
        {
            acquiredUpgrades = new List<UpgradeData>();
        }
        switch (upgradeData.upgradeType)
        {
            case UpgradeType.WeaponUpgrade:
                weaponManager.UpgradeWeapon(upgradeData);
                break;
            case UpgradeType.ItemUpgrade:
                break;
            case UpgradeType.WeaponUnlock:
                weaponManager.AddWeapon(upgradeData.weaponData);
                break;
            case UpgradeType.ItemUnlock:
                break;
        }
        acquiredUpgrades.Add(upgradeData);
        upgrades.Remove(upgradeData);
    }
   
}
