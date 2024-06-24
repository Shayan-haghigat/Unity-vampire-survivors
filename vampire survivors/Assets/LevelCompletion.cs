using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletion : MonoBehaviour
{
    [SerializeField] float timeToCompleteLevel;
    StageTime stagetime;
    PuaseManager puaseManager;
    [SerializeField] GameWinPanel LevelCompletePanel;

    private void Awake()
    {
        stagetime = GetComponent<StageTime>();
        puaseManager = FindObjectOfType<PuaseManager>();
        LevelCompletePanel = FindObjectOfType<GameWinPanel>(true);
    }

    private void Update()
    {
        if (stagetime.time > timeToCompleteLevel)
        {
            puaseManager.PuaseGame();
            LevelCompletePanel.gameObject.SetActive(true);
        }
    }
}
