using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void QuitAppllication (){
        Debug.Log("Application Quited !");
        Application.Quit();
    }
}
