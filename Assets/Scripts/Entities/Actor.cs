using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    public abstract void OnSelect();

    public abstract void OnDeselect();

    public abstract void GiveRightClickOrder(Vector3 clickPosition);
}
