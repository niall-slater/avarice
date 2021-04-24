using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBarController : MonoBehaviour
{
    public GameObject BuildMenu;
    public GameObject GateMenu;

    void Start()
    {
        UIEventHub.Instance.OnSelectionChanged += HandleSelectionChange;
        CloseMenus();
    }

    private void HandleSelectionChange(List<Actor> selected)
    {
        if (selected.Count == 1)
        {
            if (selected[0] is Builder)
            {
                OpenBuildMenu();
                return;
            }

            if (selected[0] is Gate gate)
            {
                OpenGateMenu(gate);
                return;
            }
        }

        if (selected.Count > 1)
        {

        }

        CloseMenus();
    }

    private void CloseMenus()
    {
        BuildMenu.SetActive(false);
        GateMenu.SetActive(false);
    }

    private void OpenBuildMenu()
    {
        GateMenu.SetActive(false);
        BuildMenu.SetActive(true);
    }

    private void OpenGateMenu(Gate gate)
    {
        GateMenu.SetActive(true);
        GateMenu.GetComponent<GateMenu>().SelectedGate = gate;
        BuildMenu.SetActive(false);
    }
}
