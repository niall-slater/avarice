using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateControl : MonoBehaviour
{
    public GateMenu GateMenu;

    public void ToggleGate()
    {
        GateMenu.SelectedGate.Toggle();
    }
}
