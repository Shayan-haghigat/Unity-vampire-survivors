using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuaseManager : MonoBehaviour
{
    private void Start()
    {
        OnPuaseGame(); //UnPause bayad bashe
    }

    public void PuaseGame()
    {
        Time.timeScale = 0f;
    }
    public void OnPuaseGame()
    {
        Time.timeScale = 1f;
    }
}
