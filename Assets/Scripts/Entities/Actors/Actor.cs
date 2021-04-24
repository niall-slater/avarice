using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameVariables;

public abstract class Actor : MonoBehaviour
{
    public TEAM Team;

    public abstract void OnSelect();

    public abstract void OnDeselect();

    public abstract void GiveRightClickOrder(Vector3 clickPosition);

    public bool Alive => HP > 0;

    public virtual Sprite GetSprite()
    {
        return GetComponentInChildren<SpriteRenderer>().sprite;
    }

    public float HP = 5f;

    public virtual void Hurt(float amount)
    {
        HP -= amount;

        if (HP <= 0f)
        {
            Kill();
        }
    }

    protected virtual void Kill()
    {
        Destroy(gameObject);
    }
}
