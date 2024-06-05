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
    int TO_LEVEL_UP
    {
        get
        {
            return level * 1000;
        }
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
        acquiredUpgrades.Add(upgradeData);
        upgrades.Remove(upgradeData);
    }
}
