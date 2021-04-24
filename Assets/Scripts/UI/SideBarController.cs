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

    private void HandleSelectionChange(Actor selected)
    {
        if (selected is Builder builder)
        {
            OpenBuildMenu();
            return;
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
