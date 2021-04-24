using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    public float HP = 100f;

    public bool Open = false;

    public SpriteRenderer spriteRenderer;
    public Sprite SpriteClosed;
    public Sprite SpriteOpen;

    private void Start()
    {
        SelectSprite(Open);
    }

    public void Toggle()
    {
        Open = !Open;
        SelectSprite(Open);

        GetComponent<Collider>().enabled = !Open;
    }

    private void SelectSprite(bool open)
    {
        if (open)
            spriteRenderer.sprite = SpriteOpen;
        else
            spriteRenderer.sprite = SpriteClosed;
    }
}
