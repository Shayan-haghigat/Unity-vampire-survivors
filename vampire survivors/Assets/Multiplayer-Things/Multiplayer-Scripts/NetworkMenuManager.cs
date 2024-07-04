using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkMenuManager : MonoBehaviour
{
    public string networkMenuSceneName = "NetworkMenu"; // Name of your network menu scene

    public void LoadNetworkMenu()
    {
        SceneManager.LoadScene(networkMenuSceneName);
    }
}