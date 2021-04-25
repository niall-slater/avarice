using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip ClipExplosion;
    public AudioClip ClipMonsterDeath;
    public AudioClip ClipMarineDeath;

    // Start is called before the first frame update
    void Start()
    {
        ActorEventHub.Instance.OnActorDestroyed += HandleActorDestroyed;
    }

    private void HandleActorDestroyed(Actor actor)
    {
        if (actor is Building)
        {
            AudioSource.PlayClipAtPoint(ClipExplosion, actor.transform.position);
        }

        if (actor is Monster)
        {
            AudioSource.PlayClipAtPoint(ClipMonsterDeath, actor.transform.position);
        }

        if (actor is Marine)
        {
            AudioSource.PlayClipAtPoint(ClipMarineDeath, actor.transform.position);
        }
    }

}
