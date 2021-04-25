using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BioBomb : Building
{
    private float _fuseTicker;
    
    void Start()
    {
        _fuseTicker = GameVariables.BIO_BOMB_FUSE;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        _fuseTicker -= Time.deltaTime;

        if (_fuseTicker <= 0)
        {
            Detonate();
        }
    }

    private void Detonate()
    {
        Debug.Log("BIO BOMB DETONATED");
        GameController.Win();
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
