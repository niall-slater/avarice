using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameVariables;

public abstract class Actor : MonoBehaviour
{
    public TEAM Team;

    public abstract void OnSelect();

    public abstract void OnDeselect();

    public abstract void GiveRightClickOrder(Vector3 clickPosition);

    public bool Alive => HP > 0;

    public string ActorName = "Actor";

    public int KillCount;

    public virtual Sprite GetSprite()
    {
        return GetComponentInChildren<SpriteRenderer>().sprite;
    }

    public float HP = 5f;

    public virtual void Hurt(float amount, Actor damageSource)
    {
        HP -= amount;

        if (HP <= 0f)
        {
            Kill(damageSource);
        }
    }

    protected virtual void Kill(Actor killer)
    {
        Destroy(gameObject);
        ActorEventHub.Instance.RaiseOnActorDestroyed(this, killer);
    }
}
