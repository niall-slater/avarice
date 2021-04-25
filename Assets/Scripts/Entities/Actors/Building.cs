using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : Actor
{
    protected float HurtEffectCooldown = 1f;
    protected float _hurtEffectCooldownTicker;

    public float Cost;

    protected virtual void Update()
    {
        if (_hurtEffectCooldownTicker > 0)
            _hurtEffectCooldownTicker -= Time.deltaTime;
    }

    public override void Hurt(float amount)
    {
        base.Hurt(amount);

        if (_hurtEffectCooldownTicker <= 0)
        {
            Instantiate(Resources.Load<GameObject>(PrefabPaths.DebrisPrefab), transform, false);
            _hurtEffectCooldownTicker = HurtEffectCooldown;
        }
    }
}
