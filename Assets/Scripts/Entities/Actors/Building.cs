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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Caravan"))
        {
            if (!(this is Mine))
            {
                Kill(null);
            }
        }
        else if (collision.gameObject.CompareTag("Monster"))
        {
            if (collision.gameObject.name == "GiantMonster")
            {
                Hurt(GameVariables.GIANT_MONSTER_ATTACKPOWER, null);
            }
            else
            {
                Hurt(GameVariables.DEFAULT_MONSTER_ATTACKPOWER, null);
            }
        }
    }

    public override void Hurt(float amount, Actor perpetrator)
    {
        base.Hurt(amount, perpetrator);

        if (_hurtEffectCooldownTicker <= 0)
        {
            Instantiate(Resources.Load<GameObject>(PrefabPaths.DebrisPrefab), transform, false);
            _hurtEffectCooldownTicker = HurtEffectCooldown;
        }
    }
}
