using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BioBombTimer : MonoBehaviour
{
    public TextMeshProUGUI Timer;
    private BioBomb _bomb;

    void Awake()
    {
        ScoreEventHub.Instance.OnBioBombBuilt += HandleBuilt;
        ActorEventHub.Instance.OnActorDestroyed += HandleActorDestroyed;
        ScoreEventHub.Instance.OnBioBombDetonation += HandleDetonation;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        ScoreEventHub.Instance.OnBioBombBuilt -= HandleBuilt;
        ActorEventHub.Instance.OnActorDestroyed -= HandleActorDestroyed;
        ScoreEventHub.Instance.OnBioBombDetonation -= HandleDetonation;
    }

    private void HandleBuilt(BioBomb bomb)
    {
        gameObject.SetActive(true);
        if (_bomb == null)
            _bomb = bomb;
    }

    private void HandleActorDestroyed(Actor actor, Actor killer)
    {
        if (actor as BioBomb == _bomb)
        {
            _bomb = null;
            var nextBomb = Map.Buildings.Find(x => x is BioBomb) as BioBomb;
            if (nextBomb == null)
            {
                Reset();
            }
            else
            {
                _bomb = nextBomb;
            }
        }
    }

    private void HandleDetonation(BioBomb bomb)
    {
        Reset();
    }

    private void Reset()
    {
        Timer.text = $"BIO BOMB\nDETONATION ABORTED";
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        int minutes = Mathf.FloorToInt(_bomb.SecondsLeft / 60F);
        int seconds = Mathf.FloorToInt(_bomb.SecondsLeft - minutes * 60);
        string formattedTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        Timer.text = $"BIO BOMB DETONATES IN\n{formattedTime}";
    }
}
