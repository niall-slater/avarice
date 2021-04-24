using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedEffect : MonoBehaviour
{
    public float Lifetime = 1f;

    void Update()
    {
        Lifetime -= Time.deltaTime;

        if (Lifetime < 0)
            Destroy(gameObject);
    }
}
