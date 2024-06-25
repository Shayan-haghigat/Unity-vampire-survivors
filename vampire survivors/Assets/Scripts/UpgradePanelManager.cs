using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] List<UpgradeButton> upgradeButtons;
    PuaseManager pauseManager;

    private void Awake()
    {
        pauseManager = GetComponent<PuaseManager>();
    }

    private void Start()
    {
        HideButtons();
    }

    public void OpenPanel(List<UpgradeData> upgradeDatas)
    {
        Clean();
        pauseManager.PuaseGame();
        panel.SetActive(true);

        for (int i = 0; i < upgradeDatas.Count && i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].Set(upgradeDatas[i]);
        }
    }

    public void Clean()
    {
        foreach (var button in upgradeButtons)
        {
            button.Clean();
        }
    }

    public void Upgrade(int pressedButtonID)
    {
        if (pressedButtonID < 0 || pressedButtonID >= upgradeButtons.Count)
        {
            Debug.LogWarning("Invalid button ID pressed.");
            return;
        }

        GameManager.Instance.playerTransform.GetComponent<Level>().Upgrade(pressedButtonID);
        ClosePanel();
    }

    public void ClosePanel()
    {
        HideButtons();
        pauseManager.OnPuaseGame();
        panel.SetActive(false);
    }

    private void HideButtons()
    {
        foreach (var button in upgradeButtons)
        {
            button.gameObject.SetActive(false);
        }
    }
}