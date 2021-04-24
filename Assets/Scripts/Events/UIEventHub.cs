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
    /// A monster spawns.
    /// </summary>
    public delegate void SelectionChanged(Actor selected); public event SelectionChanged OnSelectionChanged;
    public void RaiseOnSelectionChanged(Actor selected) { OnSelectionChanged?.Invoke(selected); }
}
