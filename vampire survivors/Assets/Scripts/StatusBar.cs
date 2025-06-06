using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    // Reference to the Transform of the UI element that visually represents the bar (e.g., the filled part of a health bar).
    [SerializeField] Transform bar ;

    // Updates the status bar's visual representation based on current and maximum values.
    public void Setstatus(int currentHp, int maxHp)
    {
        // Calculate the fill percentage of the bar.
        float state = (float)currentHp;
        state /= maxHp;

        // Ensure the fill percentage is not negative.
        if (state < 0f)
        {
            state = 0f;
        }
        // Update the local scale of the bar Transform to reflect the fill percentage.
        // This typically scales the bar along one axis (e.g., X-axis for a horizontal bar).
        // The Y and Z scale are kept constant (4f in this case, which might need adjustment depending on the UI setup).
        bar.transform.localScale = new Vector3(state, 4f, 4f);

    }
}
