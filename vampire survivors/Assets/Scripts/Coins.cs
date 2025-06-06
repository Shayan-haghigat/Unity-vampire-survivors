using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    // Reference to the DataContainer to store and retrieve the player's coin count.
    [SerializeField] DataContainer data;
    // UI Text element to display the current coin count.
    [SerializeField] TMPro.TextMeshProUGUI coinsCountText;

    // Adds a specified amount of coins to the player's total.
    public void Add(int count)
    {
        data.coins += count; // Increase the coin count in the DataContainer.
        // Update the UI text to reflect the new coin count.
        coinsCountText.text = "Coins : " + data.coins.ToString();
    }
}
