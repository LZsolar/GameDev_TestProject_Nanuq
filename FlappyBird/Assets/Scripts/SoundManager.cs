using UnityEngine;
using System.Collections.Generic;
using System;

public enum audioName
{
    BirdFly = 0,
    ScoreAdd =1,
    GameEnd =2
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance => _instance;

    AudioSource audioSource;

    public List<AudioClip> audioList;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void playAudio(int soundNumber)
    {
        audioSource.PlayOneShot(audioList[soundNumber]);
    }
}
