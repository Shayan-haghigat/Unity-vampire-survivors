using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    PuaseManager puaseManager;
    public void Awake()
    {
        puaseManager = GetComponent<PuaseManager>();
    }
    public void OpenPanel()
    {
        puaseManager.PuaseGame();
        panel.SetActive(true);
    }
    public void ClosePanel()
    {
        puaseManager.OnPuaseGame();
        panel.SetActive(false);
    }
}
