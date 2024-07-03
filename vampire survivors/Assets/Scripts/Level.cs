using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    int experience = 0;
    int level = 1;
    [SerializeField] ExperienceBar experienceBar;
    [SerializeField] UpgradePanelManager upgradePanel;
    [SerializeField] List<UpgradeData> upgrades;
    List<UpgradeData> selectedUpgrades = new List<UpgradeData>();
    [SerializeField] List<UpgradeData> acquiredUpgrades = new List<UpgradeData>();
    WeaponManager weaponManager;
    PassiveItems passiveItems;
    [SerializeField] List<UpgradeData> upgradesAvailableOnStart;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        passiveItems = GetComponent<PassiveItems>();
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
        if (UpgradesToAdd == null)
        {
            return;
        }
        this.upgrades.AddRange(UpgradesToAdd);
    }

    private void Start()
    {
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
        experienceBar.SetLevelText(level);
        AddUpgradesIntoTheListOfAvailableUpgrades(upgradesAvailableOnStart);
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUP();
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
    }

    public void CheckLevelUP()
    {
        if (experience >= TO_LEVEL_UP)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        selectedUpgrades.Clear();
        selectedUpgrades.AddRange(GetUpgrades(4));
        upgradePanel.OpenPanel(selectedUpgrades);
        experience -= TO_LEVEL_UP;
        level += 1;
        experienceBar.SetLevelText(level);
    }
    public void ShuffleUpgrades()
    {
        for (int i = upgrades.Count-1;i>0;i--)
        {
            int x = Random.Range(0,i+1);
            UpgradeData shufffeElement = upgrades[i];
            upgrades[i] = upgrades[x];
            upgrades[x]= shufffeElement;
        }
    }
    public List<UpgradeData> GetUpgrades(int count)
    {
        ShuffleUpgrades();
        List<UpgradeData> upgrade_list = new List<UpgradeData>();

        if (count > upgrades.Count)
        {
            count = upgrades.Count;
        }

        for (int i = 0; i < count; i++)
        {
           // int randomIndex = Random.Range(0, upgrades.Count);
            upgrade_list.Add(upgrades[i]);
           // upgrades.RemoveAt(randomIndex);
        }

        return upgrade_list;
    }

    public void Upgrade(int selectedUpgradeID)
    {
        if (selectedUpgradeID < 0 || selectedUpgradeID >= selectedUpgrades.Count)
        {
            Debug.LogWarning("Invalid upgrade ID selected.");
            return;
        }

        UpgradeData upgradeData = selectedUpgrades[selectedUpgradeID];

        switch (upgradeData.upgradeType)
        {
            case UpgradeType.WeaponUpgrade:
                weaponManager.UpgradeWeapon(upgradeData);
                break;
            case UpgradeType.ItemUpgrade:
                passiveItems.UpgradeItem(upgradeData);
                break;
            case UpgradeType.WeaponUnlock:
                weaponManager.AddWeapon(upgradeData.weaponData);
                break;
            case UpgradeType.ItemUnlock:
                passiveItems.Equip(upgradeData.item);
                AddUpgradesIntoTheListOfAvailableUpgrades(upgradeData.item.upgrades);
                break;
        }

        acquiredUpgrades.Add(upgradeData);
    }
}
