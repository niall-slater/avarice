using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowlyScroll : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += Vector3.left * Time.deltaTime * speed;
    }
}
