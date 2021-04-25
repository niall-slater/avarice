using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageField : Actor
{
    public float DamagePerFrame;

    public override void GiveRightClickOrder(Vector3 clickPosition)
    {
    }

    public override void OnDeselect()
    {
    }

    public override void OnSelect()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var other = collision.gameObject.GetComponent<Actor>();
        if (other == null)
            return;

        other.Hurt(DamagePerFrame, this);
    }
}
