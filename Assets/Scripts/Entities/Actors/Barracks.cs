﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Barracks : Building
{
    public float BuilderCost = 120f;
    public float MarineCost = 20f;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void ResetTicker()
    {
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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
