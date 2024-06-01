using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGameOver : MonoBehaviour
{
    public GameObject GameOverPanel;
    [SerializeField] GameObject weoponParent;
 public void GameOver()
 {

    Debug.Log("Game Over");
   GetComponent<PlayerMove>().enabled = false ;
    GameOverPanel.SetActive(true);
    weoponParent.SetActive(false);
 }
}
