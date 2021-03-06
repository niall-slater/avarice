using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : Building
{
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

        gameObject.layer = Open ? LayerMask.NameToLayer("PassableColliders") : LayerMask.NameToLayer("Buildings");
    }

    private void SelectSprite(bool open)
    {
        if (open)
            spriteRenderer.sprite = SpriteOpen;
        else
            spriteRenderer.sprite = SpriteClosed;
    }

    public override void OnSelect()
    {
    }

    public override void OnDeselect()
    {
    }

    public override void GiveRightClickOrder(Vector3 clickPosition)
    {
    }
}
