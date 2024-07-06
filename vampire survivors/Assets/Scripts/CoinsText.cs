using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsText : MonoBehaviour
{
    [SerializeField]  DataContainer _dataContainer;
    private TMPro.TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Update()
    {
        _text.text = "  Coins:"+ _dataContainer.coins.ToString();
        
    }
}
