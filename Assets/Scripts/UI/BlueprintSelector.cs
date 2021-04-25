using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlueprintSelector : MonoBehaviour
{
    public Building BlueprintPrefab;

    private void Start()
    {
        var text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = $"${BlueprintPrefab.Cost}\n{text.text}";
    }

    public void Select()
    {
        UIEventHub.Instance.RaiseOnBlueprintSelected(BlueprintPrefab);
        BuildingFactory.Instance.CurrentBlueprint = BlueprintPrefab;
    }
}
