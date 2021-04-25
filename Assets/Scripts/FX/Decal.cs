using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decal : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float lifetime;

    public bool Alive;
    private float lifetimeTicker;

    private Color color;

    private DecalManager _parent;

    public void Reinitialise(Vector3 position, Sprite sprite, DecalManager parent)
    {
        _parent = parent;
        lifetimeTicker = 0f;
        transform.position = position;

        if (sprite != null)
            spriteRenderer.sprite = sprite;

        var rot = 0f;
        if (Random.value < .25)
            rot = 90;
        else if (Random.value < .5)
            rot = 180;
        else if (Random.value < .75)
            rot = 270;

        transform.rotation = Quaternion.Euler(0, 0, rot);

        color = Color.white;
        color.a = 1f;
        Alive = true;
        gameObject.SetActive(true);
    }

    void Update()
    {
        lifetimeTicker += Time.deltaTime;

        if (lifetimeTicker > lifetime)
        {
            lifetimeTicker = lifetime;
        }

        color.a = Mathf.Lerp(1f, 0f, lifetimeTicker / lifetime);
        spriteRenderer.color = color;

        if (lifetimeTicker == lifetime)
        {
            Kill();
        }
    }

    public void Kill(DecalManager parent = null)
    {
        if (parent != null)
            _parent = parent;
        _parent.RefreshDecalCount();
        Alive = false;
        gameObject.SetActive(false);
    }
}
