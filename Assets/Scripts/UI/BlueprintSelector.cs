using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintSelector : MonoBehaviour
{
    public Building BlueprintPrefab;

    public void Select()
    {
        UIEventHub.Instance.RaiseOnBlueprintSelected(BlueprintPrefab);
        BuildingFactory.Instance.CurrentBlueprint = BlueprintPrefab;
    }
}
