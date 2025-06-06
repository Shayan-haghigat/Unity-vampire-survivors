using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // Current experience points.
    int experience = 0;
    // Current level.
    int level = 1;
    // UI element to display experience progress.
    [SerializeField] ExperienceBar experienceBar;
    // UI panel for selecting upgrades upon leveling up.
    [SerializeField] UpgradePanelManager upgradePanel;
    // List of all possible upgrades.
    [SerializeField] List<UpgradeData> upgrades;
    // List of currently offered upgrades to the player.
    List<UpgradeData> selectedUpgrades = new List<UpgradeData>();
    // List of upgrades the player has acquired.
    [SerializeField] List<UpgradeData> acquiredUpgrades = new List<UpgradeData>();
    // Reference to the WeaponManager to handle weapon upgrades and unlocks.
    WeaponManager weaponManager;
    // Reference to PassiveItems to handle item upgrades and unlocks.
    PassiveItems passiveItems;
    // List of upgrades available to the player from the start of the game.
    [SerializeField] List<UpgradeData> upgradesAvailableOnStart;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        passiveItems = GetComponent<PassiveItems>();
    }

    // Calculates the experience required to reach the next level.
    int TO_LEVEL_UP
    {
        get
        {
            return level * 1000; // Example: Level 1 needs 1000 XP, Level 2 needs 2000 XP, etc.
        }
    }

    // Adds a list of upgrades to the pool of available upgrades.
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
        // Initialize the experience bar and level display.
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
        experienceBar.SetLevelText(level);
        // Add starting upgrades to the available pool.
        AddUpgradesIntoTheListOfAvailableUpgrades(upgradesAvailableOnStart);
    }

    // Adds experience points and checks for level up.
    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUP();
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
    }

    // Checks if the player has enough experience to level up.
    public void CheckLevelUP()
    {
        if (experience >= TO_LEVEL_UP)
        {
            LevelUp();
        }
    }

    // Handles the level up process.
    private void LevelUp()
    {
        selectedUpgrades.Clear();
        // Gets a list of random upgrades to offer the player.
        selectedUpgrades.AddRange(GetUpgrades(4)); // Offers 4 upgrade options.
        // Opens the upgrade panel to let the player choose an upgrade.
        upgradePanel.OpenPanel(selectedUpgrades);
        experience -= TO_LEVEL_UP; // Resets experience for the next level.
        level += 1; // Increases the player's level.
        experienceBar.SetLevelText(level); // Updates the level display.
    }
    // Shuffles the list of available upgrades to provide random choices.
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
    // Gets a specified number of unique random upgrades from the available pool.
    public List<UpgradeData> GetUpgrades(int count)
    {
        ShuffleUpgrades(); // Shuffle to ensure randomness.
        List<UpgradeData> upgrade_list = new List<UpgradeData>();

        if (count > upgrades.Count)
        {
            count = upgrades.Count; // Ensure we don't try to get more upgrades than available.
        }

        for (int i = 0; i < count; i++)
        {
           // int randomIndex = Random.Range(0, upgrades.Count);
            upgrade_list.Add(upgrades[i]);
           // upgrades.RemoveAt(randomIndex); // This line was commented out, if active it would prevent duplicate offerings in the same level up.
        }

        return upgrade_list;
    }

    // Applies the selected upgrade.
    public void Upgrade(int selectedUpgradeID)
    {
        if (selectedUpgradeID < 0 || selectedUpgradeID >= selectedUpgrades.Count)
        {
            Debug.LogWarning("Invalid upgrade ID selected.");
            return;
        }

        UpgradeData upgradeData = selectedUpgrades[selectedUpgradeID];

        // Applies the upgrade based on its type (weapon upgrade, item upgrade, weapon unlock, item unlock).
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
                // When a new item is unlocked, its specific upgrades are added to the available pool.
                AddUpgradesIntoTheListOfAvailableUpgrades(upgradeData.item.upgrades);
                break;
        }

        acquiredUpgrades.Add(upgradeData); // Tracks the acquired upgrade.
    }
}
