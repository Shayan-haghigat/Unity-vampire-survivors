using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip musicOnStart;

     AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        Play(musicOnStart,true); 
    }
    AudioClip swithTo;
    public void Play(AudioClip music, bool interrupt = false)
    {
        if (interrupt == true)
        {
            volume = 1.0f;
            audioSource.volume = volume;
            audioSource.clip = music;
            audioSource.Play();
        }
        else
        {
        swithTo = music;
        StartCoroutine(SmoothSwitchMusic());

        }
    }
    float volume;
    [SerializeField] float timeToSwitch;
    IEnumerator SmoothSwitchMusic()
    {
        volume = 1.0f;
        while (volume>0f)
        {
            volume -= Time.deltaTime / timeToSwitch;
            if (volume < 0f) { volume = 0f; }
            audioSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }
      Play(swithTo,true);
    }
}
