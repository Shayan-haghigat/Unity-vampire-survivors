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
            float divisor = progressTimeRate * progressPerSplit;
            if (divisor <= 0f) // Check if divisor is zero or negative
            {
                // Log a warning and use a default safe divisor
                Debug.LogWarning("StageProgress: progressTimeRate * progressPerSplit resulted in a non-positive value. Defaulting divisor. Check values in Inspector.");
                // Fallback to a known safe calculation, e.g., based on typical default values or a hardcoded safe value.
                // If original defaults were 30 and 0.2, their product is 6.
                divisor = 6f;
                // Alternatively, if progressTimeRate and progressPerSplit could be independently problematic:
                // float safeProgressTimeRate = progressTimeRate <= 0f ? 30f : progressTimeRate;
                // float safeProgressPerSplit = progressPerSplit <= 0f ? 0.2f : progressPerSplit;
                // divisor = safeProgressTimeRate * safeProgressPerSplit;
                // For simplicity, we'll just use a hardcoded safe product if the original product is bad.
            }
            return 1f + stagetime.time / divisor;
        }
    }
}
