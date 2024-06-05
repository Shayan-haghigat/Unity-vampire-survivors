using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMassage : MonoBehaviour
{
    [SerializeField] float SavedTLL = 2f;
    float TimetoLeave = 2f; //این کلاس برای حذف همون دمیج هایی هستش که تو صفحه نشون میده

    private void OnEnable() {
        TimetoLeave = SavedTLL;
    }
    private void Update() {
        TimetoLeave -= Time.deltaTime;
        if (TimetoLeave < 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
