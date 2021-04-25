using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DecalManager : MonoBehaviour
{
    public GameObject DecalPrefab;

    public int DecalCap;

    public Sprite BloodMarine;
    public Sprite BloodMonster;

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

        if (actor is Monster)
        {
            SpawnDecal(pos, BloodMonster);
            return;
        }
        if (actor is Marine)
        {
            SpawnDecal(pos, BloodMarine);
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

    private void SpawnDecal(Vector3 position, Sprite sprite)
    {
        var decal = GetFreeDecalFromPool();

        if (decal == null)
        {
            decal = _decalPool[0];
        }

        decal.Reinitialise(position, sprite, this);

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
