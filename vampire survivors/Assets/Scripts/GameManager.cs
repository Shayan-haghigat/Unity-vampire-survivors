using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  // Singleton pattern: Ensures that there is only one instance of the GameManager throughout the game.
  // This allows other scripts to easily access the GameManager and its properties.
  public static GameManager Instance;
  // همون سینگلتون هستش 
    private void Awake()
    {
        Instance = this;
    }
    // Reference to the player's transform.
    // Used by other scripts to get the player's position, rotation, etc.
    public Transform playerTransform;
}
