using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enum defining the types of persistent upgrades available to the player.
public enum PlayerPresistentUpgrades
{
    Hp,     // Health upgrade
    Damage  // Damage upgrade
}

// Serializable class to store information about each player upgrade.
[Serializable]
public class PlayerUpgrades
{
    // Type of the persistent upgrade.
    public PlayerPresistentUpgrades presistentUpgrades;
    // Current level of this upgrade.
    public int level = 0;
    // Maximum attainable level for this upgrade.
    public int max_level = 10;
    // Cost to upgrade to the next level.
    public int costToUpgrade = 100;
}

// ScriptableObject to store and manage persistent game data.
// This allows data to be saved and loaded across game sessions.
[CreateAssetMenu]
public class DataContainer : ScriptableObject
{
    // Player's current coin balance.
    public int coins;
    // List of all persistent player upgrades.
    public List<PlayerUpgrades> upgrades;
    // Data of the character currently selected by the player.
    public CharecterData selectedCharacter;
    // Player's highest achieved score.
    public int highestScore;

    // Retrieves the current level of a specific persistent upgrade.
    public int GetUpgradeLevel(PlayerPresistentUpgrades presistentUpgrades)
    {
        // Assumes the 'upgrades' list is ordered by the enum values.
        return upgrades[(int)presistentUpgrades].level;
    }

    // Sets the currently selected character.
    public void SetSelectedCharecter(CharecterData charecter)
    {
        selectedCharacter = charecter;
    }

    // Retrieves the player's highest score.
    public int GetHighestScore()
    {
        return highestScore;
    }

    // Sets the player's highest score, only if the new score is higher.
    public void SetHighestScore(int score)
    {
        if (score > highestScore)
        {
            highestScore = score;
        }
    }
}