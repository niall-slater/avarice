using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BioBomb : Building
{
    private float _fuseTicker;

    public GameObject ExplosionEffectPrefab;

    private bool _detonated;

    void Start()
    {
        _fuseTicker = GameVariables.BIO_BOMB_FUSE;

        PopUpManager.CreatePopup($"BIO BOMB ARMED:\n{Mathf.RoundToInt(_fuseTicker)} SECONDS");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (_detonated)
            return;

        _fuseTicker -= Time.deltaTime;

        if (_fuseTicker <= 0)
        {
            _detonated = true;
            Detonate();
        }
    }

    private void Detonate()
    {
        Debug.Log("BIO BOMB DETONATED");
        Instantiate(ExplosionEffectPrefab, transform);
        ScoreEventHub.Instance.RaiseOnBioBombDetonation();
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
