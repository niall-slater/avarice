using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintRenderer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public void Refresh(Sprite spriteToDisplay)
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = spriteToDisplay;
    }

    public void Clear()
    {
        spriteRenderer.sprite = null;
        spriteRenderer.enabled = false;
    }
}
