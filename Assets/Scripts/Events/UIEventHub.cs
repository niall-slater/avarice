using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventHub
{
    private static UIEventHub _instance;

    public static UIEventHub Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIEventHub();
            }
            return _instance;
        }
    }

    public static void Destroy() { _instance = null; }

    /// <summary>
    /// The currently-selected actor changes.
    /// </summary>
    public delegate void SelectionChanged(List<Actor> selected); public event SelectionChanged OnSelectionChanged;
    public void RaiseOnSelectionChanged(List<Actor> selected) { OnSelectionChanged?.Invoke(selected); }

    /// <summary>
    /// The player clicks on a blueprint they want to build.
    /// </summary>
    public delegate void BlueprintSelected(Building blueprint); public event BlueprintSelected OnBlueprintSelected;
    public void RaiseOnBlueprintSelected(Building blueprint) { OnBlueprintSelected?.Invoke(blueprint); }
}
