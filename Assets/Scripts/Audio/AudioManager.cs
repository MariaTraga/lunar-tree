using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance;

    public static AudioManager Instance { get { return _instance; } }

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

    [SerializeField] GameObject audioSourcePrefab;
    [SerializeField] int audioSourceCount;

    List<AudioSource> audioSources;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        audioSources = new List<AudioSource>();
        for(int i = 0; i < audioSourceCount; i++)
        {
            GameObject gameObject = Instantiate(audioSourcePrefab, transform);
            gameObject.transform.localPosition = Vector3.zero;
            audioSources.Add(gameObject.GetComponent<AudioSource>());
        }
    }

    public void Play(AudioClip audioClip)
    {
        if(audioClip == null) { return; }

        AudioSource audioSource = GetFreeAudioSource();
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    private AudioSource GetFreeAudioSource()
    {
        foreach (AudioSource source in audioSources)
        {
            if (source.isPlaying == false)
            {
                return source;
            }
        }
        return audioSources[0];
    }
}
