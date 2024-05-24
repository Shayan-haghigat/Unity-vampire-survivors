using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    [SerializeField] Transform bar ;

    public void Setstatus(int currentHp, int maxHp)
    {

        float state = (float)currentHp;
        state /= maxHp;
        if (state < 0f)
        {
            state = 0f;
        }
        bar.transform.localScale = new Vector3(state, 4f, 4f);

    }
}
