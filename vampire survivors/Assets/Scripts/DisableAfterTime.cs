using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
    float timetoDisable = 0.2f;
    float timer ;
    // Start is called before the first frame update
    private void OnEnable() {
        timer = timetoDisable;
    }
    private void LateUpdate() {
        timer -= Time.deltaTime;
        if(timer <= 0f){
            gameObject.SetActive(false);
        }
    }
}
