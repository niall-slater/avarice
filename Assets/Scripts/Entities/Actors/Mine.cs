using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mine : Building
{
    private float SpawnCreatureIntervalVariance = 1f;
    private float SpawnCreatureInterval = 5f;
    private float SpawnCreatureChance = 0.5f;
    private float _spawnCreatureTicker;

    public float OreValue;
    public float OreGrowthRate;
    public float OreCap;

    private float DrillSpeed = 18f;
    public float MiningDepth;

    public TextMeshProUGUI OreReadout;

    // Start is called before the first frame update
    void Start()
    {
        ResetTicker();
        MiningDepth = 5000f;
    }

    private void ResetTicker()
    {
        _spawnCreatureTicker = SpawnCreatureInterval + UnityEngine.Random.Range(-SpawnCreatureIntervalVariance / 2f, SpawnCreatureIntervalVariance / 2f);
    }

    public float CollectOre(float amount)
    {
        if (OreValue >= amount)
        {
            OreValue -= amount;
            return amount;
        }

        var result = OreValue;
        OreValue = 0;
        return result;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        MiningDepth += DrillSpeed * Time.deltaTime;

        if (GameController.MaxDepthReached < MiningDepth)
        {
            GameController.MaxDepthReached = MiningDepth;
        }

        UpdateSpawning();

        if (OreValue < OreCap)
        {
            var growth = OreGrowthRate * Time.deltaTime;
            OreValue += growth;
        }
        else
        {
            OreValue = OreCap;
        }

        OreReadout.text = $"{Mathf.RoundToInt(OreValue)}T";
    }

    private void UpdateSpawning()
    {
        if (MiningDepth < GameVariables.DEPTH_LEVEL_0)
            return;

        if (MiningDepth < GameVariables.DEPTH_LEVEL_1)
        {
             SpawnCreatureIntervalVariance = .5f;
             SpawnCreatureInterval = 3f;
             SpawnCreatureChance = 0.6f;
        }

        if (MiningDepth < GameVariables.DEPTH_LEVEL_2)
        {
            SpawnCreatureIntervalVariance = .4f;
            SpawnCreatureInterval = 1.5f;
            SpawnCreatureChance = 0.75f;
        }

        if (MiningDepth < GameVariables.DEPTH_LEVEL_3)
        {
            SpawnCreatureIntervalVariance = .3f;
            SpawnCreatureInterval = .6f;
            SpawnCreatureChance = 0.9f;
        }
        else
        {
            SpawnCreatureIntervalVariance = .03f;
            SpawnCreatureInterval = .05f;
            SpawnCreatureChance = 0.95f;
        }

        _spawnCreatureTicker -= Time.deltaTime;
        if (_spawnCreatureTicker < 0)
        {
            if (UnityEngine.Random.value < SpawnCreatureChance)
            {
                SpawnCreature();
            }
            ResetTicker();
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
