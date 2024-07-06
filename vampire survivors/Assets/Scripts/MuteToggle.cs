using UnityEngine;
using UnityEngine.UI;
using TMPro; // Required for TextMeshPro
using UnityEngine.Audio; // Required for controlling audio

public class MuteToggle : MonoBehaviour
{
    public Button muteButton;
    public TextMeshProUGUI muteButtonText; // TextMeshPro component
    public AudioMixer audioMixer; // AudioMixer to control the volume

    private bool isMuted = false;

    void Start()
    {
        muteButton.onClick.AddListener(ToggleMute);
        UpdateButtonText();
    }

    void ToggleMute()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            audioMixer.SetFloat("MasterVolume", -80); // Mute (set volume to a very low value)
        }
        else
        {
            audioMixer.SetFloat("MasterVolume", 0); // Unmute (set volume back to 0 dB)
        }

        UpdateButtonText();
    }

    void UpdateButtonText()
    {
        if (isMuted)
        {
            muteButtonText.text = "Unmute";
        }
        else
        {
            muteButtonText.text = "Mute";
        }
    }
}