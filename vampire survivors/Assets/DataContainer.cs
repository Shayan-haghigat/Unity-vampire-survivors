using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerPresistentUpgrades
{
    Hp,
    Damage
}

[Serializable]
public class PlayerUpgrades
{
    public PlayerPresistentUpgrades presistentUpgrades;
    public int level = 0;
    public int max_level = 10;
    public int costToUpgrade = 100;
}

[CreateAssetMenu]
public class DataContainer : ScriptableObject
{
    public int coins;
    public List<PlayerUpgrades> upgrades;
    public CharecterData selectedCharacter;
    public int highestScore; // Add this line

    public int GetUpgradeLevel(PlayerPresistentUpgrades presistentUpgrades)
    {
        return upgrades[(int)presistentUpgrades].level;
    }

    public void SetSelectedCharecter(CharecterData charecter)
    {
        selectedCharacter = charecter;
    }

    public int GetHighestScore()
    {
        return highestScore;
    }

    public void SetHighestScore(int score)
    {
        if (score > highestScore)
        {
            highestScore = score;
        }
    }
}