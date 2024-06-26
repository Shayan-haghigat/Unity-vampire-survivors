using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageProgress : MonoBehaviour
{
    public StageTime stagetime;

    public void Awake()
    {
        stagetime = GetComponent<StageTime>();
    }

    [SerializeField] float progressTimeRate = 30f;
    [SerializeField] float progressPerSplit = 0.2f;

    public float Progress
    {
        get
        {
            return 1f + stagetime.time / (progressTimeRate * progressPerSplit);
        }
    }
}
