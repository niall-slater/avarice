using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var scroll = Input.mouseScrollDelta.y;

        var movement = new Vector3(x, y, 0f);
        Camera.main.orthographicSize -= scroll;

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 1f, 12f);

        transform.position = Vector3.Lerp(transform.position, transform.position + movement, Time.deltaTime * 10f);
    }
}
