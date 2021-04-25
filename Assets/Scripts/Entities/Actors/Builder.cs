using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MovingUnit
{
    public float BuildRange = 3f;
    public GameObject BuildRangeIndicator;

    public override void OnSelect()
    {
        base.OnSelect();
        BuildRangeIndicator.SetActive(true);
    }

    public override void OnDeselect()
    {
        base.OnDeselect();
        BuildRangeIndicator.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            if (collision.gameObject.name == "GiantMonster(Clone)")
            {
                Hurt(GameVariables.GIANT_MONSTER_ATTACKPOWER, null);
            }
            else
            {
                Hurt(GameVariables.DEFAULT_MONSTER_ATTACKPOWER, null);
            }
        }
    }
}
