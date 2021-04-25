using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioBombExplosion : MonoBehaviour
{
    public SpriteRenderer SicklyOverlayRenderer;

    private Color color;
    private float ticker;
    private float duration = 5f;
    private float targetAlpha = 100f/255f;

    // Start is called before the first frame update
    void Start()
    {
        ticker = 0f;
        color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        if (ticker < duration)
        {
            ticker = Mathf.MoveTowards(ticker, duration, Time.deltaTime);
        }
        color.a = Mathf.Lerp(color.a, targetAlpha, ticker/duration);
        SicklyOverlayRenderer.color = color;
    }
}
