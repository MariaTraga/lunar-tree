using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager _instance;

    public static MusicManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    [SerializeField] AudioSource audioSource;
    [SerializeField] float timeToSwitch = 5f;

    [SerializeField] AudioClip audioClipOnStart;

    float volume;
    AudioClip switchToClip;

    private void Start()
    {
        Play(audioClipOnStart, true);
    }

    public void Play(AudioClip clipToPlay,bool interrupt = false)
    {
        if(clipToPlay == null) { return; }

        if (interrupt)
        {
            audioSource.volume = 1f;
            audioSource.clip = clipToPlay;
            audioSource.Play();
        }
        else
        {
            switchToClip = clipToPlay;
            StartCoroutine(SmoothMusicTransition());
        }
    }

    IEnumerator SmoothMusicTransition()
    {
        volume = 1f;
        while(volume > 0f)
        {
            volume -= Time.deltaTime/timeToSwitch;
            if (volume < 0f)
            {
                volume = 0f;
            }
            audioSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }

        Play(switchToClip, true);
    }
}
