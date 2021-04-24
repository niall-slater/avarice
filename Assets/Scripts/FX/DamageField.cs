using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageField : MonoBehaviour
{
    public float DamagePerFrame;

    private void OnTriggerStay2D(Collider2D collision)
    {
        var other = collision.gameObject.GetComponent<Actor>();
        if (other == null)
            return;

        other.Hurt(DamagePerFrame);
    }
}
