using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField]  DataContainer _dataContainer;
    private TMPro.TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Update()
    {
        _text.text = "  HighestScore :"+ _dataContainer.highestScore.ToString();
        
    }
}