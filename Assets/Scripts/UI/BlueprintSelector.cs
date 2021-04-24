using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintSelector : MonoBehaviour
{
    public GameObject BlueprintPrefab;

    public void Select()
    {
        BuildingFactory.Instance.CurrentBlueprint = BlueprintPrefab;
    }
}
