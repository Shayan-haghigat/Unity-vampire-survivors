using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject panel;
    PuaseManager puaseManager;
    private void Awake()
    {
        puaseManager = GetComponent<PuaseManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
         if(panel.activeInHierarchy == false)
            {
                OpenMenu();
            }
         else
            {
                CloseMenu();
            }
        }
    }
    public void CloseMenu() {
        puaseManager.OnPuaseGame();
        panel.SetActive(false);

    }
    public void OpenMenu()
    {
        puaseManager.PuaseGame();
        panel.SetActive(true );
    }
}
