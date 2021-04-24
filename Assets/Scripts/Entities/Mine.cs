using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Building
{
    public float SpawnCreatureIntervalVariance = 1f;
    public float SpawnCreatureInterval = 2.5f;
    public float SpawnCreatureChance = 0.5f;
    private float _spawnCreatureTicker;

    // Start is called before the first frame update
    void Start()
    {
        ResetTicker();
    }

    private void ResetTicker()
    {
        _spawnCreatureTicker = SpawnCreatureInterval + UnityEngine.Random.Range(-SpawnCreatureIntervalVariance / 2f, SpawnCreatureIntervalVariance / 2f);
    }

    // Update is called once per frame
    void Update()
    {
        _spawnCreatureTicker -= Time.deltaTime;
        if (_spawnCreatureTicker < 0)
        {
            if (UnityEngine.Random.value < SpawnCreatureChance)
                SpawnCreature();
            ResetTicker();
        }
    }

    private void SpawnCreature()
    {
        GameController.SpawnMonster(transform.position);
    }
}
