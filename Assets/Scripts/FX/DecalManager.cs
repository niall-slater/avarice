using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DecalManager : MonoBehaviour
{
    public GameObject DecalPrefab;

    public GameObject DeathBurstEffect;
    public GameObject DeathBurstEffect_Monster;

    public int DecalCap;

    public Sprite BloodMarine;
    public Sprite BloodMonster;
    public Sprite BloodBuilder;
    public Sprite Rubble;

    private int _decalCount;

    private List<Decal> _decalPool;

    // Start is called before the first frame update
    void Start()
    {
        FillDecalPool();

        ActorEventHub.Instance.OnActorDestroyed += HandleActorDestroyed;
    }

    private void HandleActorDestroyed(Actor actor)
    {
        var pos = actor.transform.position;

        if (actor.Team == GameVariables.TEAM.PLAYER)
        {
            Instantiate(DeathBurstEffect, pos, Quaternion.identity, null);
        }
        else
        {
            Instantiate(DeathBurstEffect_Monster, pos, Quaternion.identity, null);
        }

        if (actor is Monster)
        {
            SpawnDecal(pos, BloodMonster, actor.transform.localScale.x);
            return;
        }
        if (actor is Marine)
        {
            SpawnDecal(pos, BloodMarine);
            return;
        }
        if (actor is Builder)
        {
            SpawnDecal(pos, BloodBuilder);
            return;
        }
        if (actor is Building)
        {
            SpawnDecal(pos, Rubble);
            return;
        }
    }

    private void FillDecalPool()
    {
        _decalPool = new List<Decal>();
        for (var i = 0; i < DecalCap; i++)
        {
            var decal = GameObject.Instantiate(DecalPrefab).GetComponent<Decal>();
            decal.Kill(this);
            _decalPool.Add(decal);
        }
    }

    private void SpawnDecal(Vector3 position, Sprite sprite, float scale = 1f)
    {
        var decal = GetFreeDecalFromPool();

        if (decal == null)
        {
            decal = _decalPool[0];
        }

        decal.Reinitialise(position, sprite, this);
        decal.transform.localScale = Vector3.one * scale;

        RefreshDecalCount();
    }

    public void RefreshDecalCount()
    {
        _decalCount = _decalPool.Count(x => x.Alive);
    }

    private Decal GetFreeDecalFromPool()
    {
        return _decalPool.FirstOrDefault(x => !x.Alive);
    }
}
