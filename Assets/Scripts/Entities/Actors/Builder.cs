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
}
