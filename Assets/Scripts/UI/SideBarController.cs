using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBarController : MonoBehaviour
{
    public GameObject BuildMenu;
    public GameObject GateMenu;
    public GameObject BarracksMenu;

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

            if (selected[0] is Barracks barracks)
            {
                OpenBarracksMenu(barracks);
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
        BarracksMenu.SetActive(false);
    }

    private void OpenBuildMenu()
    {
        BuildMenu.SetActive(true);
    }

    private void OpenGateMenu(Gate gate)
    {
        GateMenu.SetActive(true);
        GateMenu.GetComponent<GateMenu>().SelectedGate = gate;
    }

    private void OpenBarracksMenu(Barracks barracks)
    {
        BarracksMenu.SetActive(true);
        BarracksMenu.GetComponent<BarracksMenu>().SelectedBarracks = barracks;
    }
}
