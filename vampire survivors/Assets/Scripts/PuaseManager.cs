using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuaseManager : MonoBehaviour
{
    public void PuaseGame()
    {
        Time.timeScale = 0f;
    }
    public void OnPuaseGame()
    {
        Time.timeScale = 1f;
    }
}
