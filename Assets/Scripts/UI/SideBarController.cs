using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBarController : MonoBehaviour
{
    public GameObject BuildMenu;

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
        }

        CloseMenus();
    }

    private void CloseMenus()
    {
        BuildMenu.SetActive(false);
    }

    private void OpenBuildMenu()
    {
        BuildMenu.SetActive(true);
    }
}
