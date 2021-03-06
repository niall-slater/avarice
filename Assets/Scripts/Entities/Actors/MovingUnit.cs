using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingUnit : Actor
{
    public float MoveSpeed = 3f;

    protected Vector3 _moveTarget;

    public GameObject SelectionHalo;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _moveTarget = transform.position;
    }

    protected virtual void FixedUpdate()
    {
        if (transform.position != _moveTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, _moveTarget, MoveSpeed * Time.deltaTime);
        }
    }

    public override void OnSelect()
    {
        SelectionHalo.SetActive(true);
    }

    public override void OnDeselect()
    {
        SelectionHalo.SetActive(false);
    }

    public override void GiveRightClickOrder(Vector3 clickPosition)
    {
        _moveTarget = clickPosition;
    }
}
