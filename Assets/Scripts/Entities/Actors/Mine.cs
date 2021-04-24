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

    public float OreValue;
    public float OreGrowthRate;
    public float OreCap;

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
    protected override void Update()
    {
        base.Update();

        _spawnCreatureTicker -= Time.deltaTime;
        if (_spawnCreatureTicker < 0)
        {
            if (UnityEngine.Random.value < SpawnCreatureChance)
                SpawnCreature();
            ResetTicker();
        }

        if (OreValue < OreCap)
        {
            var growth = OreGrowthRate * Time.deltaTime;
            OreValue += growth;
        }
        else
        {
            OreValue = OreCap;
        }
    }

    private void SpawnCreature()
    {
        GameController.SpawnMonster(transform.position);
    }

    public override void OnSelect()
    {
    }

    public override void OnDeselect()
    {
    }

    public override void GiveRightClickOrder(Vector3 clickPosition)
    {
    }
}
