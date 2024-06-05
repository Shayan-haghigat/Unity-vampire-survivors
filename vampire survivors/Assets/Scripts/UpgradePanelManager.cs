using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    PuaseManager puaseManager;
    [SerializeField] List<UpgradeButton> upgradeButtons ;
    public void Awake()
    {
        puaseManager = GetComponent<PuaseManager>();
    }

    private void Start()
    {
        HideButtons();
    }

    public void OpenPanel(List<UpgradeData> upgradeDatas)
    {
        Clean();
        puaseManager.PuaseGame();
        panel.SetActive(true);
        for (int i = 0; i < upgradeDatas.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].Set(upgradeDatas[i]);
        }
    }

    public void Clean()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].Clean();
        }
    }

    public void Upgrade(int pressedButtonID)
    {
        GameManager.Instance.playerTransform.GetComponent<Level>().Upgrade(pressedButtonID);
        ClosePanel();
    }
    public void ClosePanel()
    {
        HideButtons();
        puaseManager.OnPuaseGame();
        panel.SetActive(false);
    }

    private void HideButtons()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(false);
        }
    }
}
