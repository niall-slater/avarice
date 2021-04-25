using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScrollingText : MonoBehaviour
{
    public Transform TextRoot;

    public Transform StartPos;
    public Transform EndPos;

    public float Duration;
    private float _progress;

    private void OnEnable()
    {
        TextRoot.position = StartPos.position;
    }

    void Update()
    {
        if (_progress < Duration)
        {
            _progress = Mathf.MoveTowards(_progress, Duration, Time.deltaTime);
        }
        TextRoot.position = Vector3.Lerp(StartPos.position, EndPos.position, _progress / Duration);
    }
}
