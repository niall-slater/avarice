using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource Music;

    public AudioClip Music_Calm;
    public AudioClip Music_Intense;

    private bool muted;
    private bool mutedAll;

    // Start is called before the first frame update
    void Start()
    {
        ScoreEventHub.Instance.OnBioBombBuilt += ChangeMusic;
        UIEventHub.Instance.OnSceneReload += ChangeMusicCalm;
        DontDestroyOnLoad(gameObject);
        muted = false;
        mutedAll = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            muted = !muted;
            Music.volume = muted ? 0f : .5f;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            mutedAll = !mutedAll;
            AudioListener.volume = mutedAll ? 0f : 1f;
        }
    }

    public void SetMusicVolume(float value)
    {
        Music.volume = value;
    }

    private void ChangeMusicCalm()
    {
        Music.clip = Music_Calm;
        Music.Play();
    }

    private void ChangeMusic(BioBomb bomb)
    {
        Music.clip = Music_Intense;
        Music.Play();
    }
}
