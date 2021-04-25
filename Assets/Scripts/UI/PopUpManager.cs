using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager
{
    public static void CreatePopup(string text, Vector3 position = default, float lifetime = 3f)
    {
        if (position == default)
        {
            position = Camera.main.transform.position;
        }
        var parent = GameObject.FindGameObjectWithTag("WorldCanvas").transform;

        var result = GameObject.Instantiate(Resources.Load<GameObject>(PrefabPaths.PopUpPrefab), position, Quaternion.identity, parent).GetComponent<PopUp>();
        position.z = -1f;
        result.Initialise(text, position, lifetime);
    }
}
