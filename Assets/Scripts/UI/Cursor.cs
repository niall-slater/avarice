using Assets.Scripts.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    public LayerMask SelectionMask;

    public List<Actor> SelectedActors;

    public Building SelectedBlueprint;

    public BlueprintRenderer Hologram;

    private Rect SelectionRectangle;

    public RectTransform SelectionRectUI;

    public enum CursorState
    {
        NORMAL,
        BLUEPRINT
    }

    public CursorState CurrentState;

    // Start is called before the first frame update
    void Start()
    {
        CurrentState = CursorState.NORMAL;
        SelectedActors = new List<Actor>();
        UIEventHub.Instance.OnBlueprintSelected += HandleBlueprintSelection;
    }

    private void HandleBlueprintSelection(Building blueprint)
    {
        SelectedBlueprint = blueprint;
        if (blueprint != null)
        {
            Hologram.Refresh(blueprint.GetSprite());
            CurrentState = CursorState.BLUEPRINT;
            return;
        }

        Hologram.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        if (Input.GetMouseButtonDown(0) && CurrentState == CursorState.NORMAL)
        {
            SelectionRectangle = new Rect(transform.position, Vector2.zero);
        }

        if (Input.GetMouseButton(0) && CurrentState == CursorState.NORMAL)
        {
            var mousePos = new Vector2(transform.position.x, transform.position.y);
            SelectionRectangle.size = mousePos - SelectionRectangle.position;
            Debug.DrawLine(mousePos, SelectionRectangle.position);
            SelectionRectUI.gameObject.SetActive(true);
            SelectionRectUI.pivot = CalculatePivotForRect(SelectionRectangle);
            SelectionRectUI.position = new Vector2(SelectionRectangle.xMin, SelectionRectangle.yMin);
            SelectionRectUI.sizeDelta = new Vector2(Mathf.Abs(SelectionRectangle.width), Mathf.Abs(SelectionRectangle.height));
        }
        else
        {
            SelectionRectUI.gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonUp(0))
        {
            switch (CurrentState)
            {
                case CursorState.NORMAL:
                    if (SelectionRectangle.size.magnitude > 1f)
                    {
                        HandleSelectionRectangleRelease();
                    }
                    else
                    {
                        HandleNormalLeftClick();
                    }
                    break;
                case CursorState.BLUEPRINT:
                    HandleBlueprintLeftClick();
                    break;
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            switch (CurrentState)
            {
                case CursorState.NORMAL:
                    HandleNormalRightClick();
                    break;
                case CursorState.BLUEPRINT:
                    HandleBlueprintRightClick();
                    break;
            }
        }
    }

    private void HandleNormalRightClick()
    {
        if (SelectedActors.Count == 0)
            return;

        //TODO: give different orders depending on where we clicked
        //var hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.zero, 1f, SelectionMask, 0f);
        //var actor = hit.collider.GetComponent<Actor>();

        foreach (Actor a in SelectedActors)
            a.GiveRightClickOrder(transform.position);
    }

    private void HandleNormalLeftClick()
    {
        var cachedSetting = Physics2D.queriesHitTriggers;
        Physics2D.queriesHitTriggers = false;
        var hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.zero, 1f, SelectionMask, 0f);
        Physics2D.queriesHitTriggers = cachedSetting;

        // We hit nothing - most likely blocked by the UI
        if (!hit)
        {
            // Return and let the UI system handle it.
            return;
        }

        Deselect();
        UIEventHub.Instance.RaiseOnSelectionChanged(SelectedActors);


        if (hit.collider.name == "Terrain")
        {
            return;
        }

        var actor = hit.collider.GetComponent<Actor>();

        if (actor != null)
        {
            // Selecting actor
            SelectedActors.Add(actor);
            UIEventHub.Instance.RaiseOnSelectionChanged(SelectedActors);
            actor.OnSelect();
            return;
        }
    }

    private void HandleSelectionRectangleRelease()
    {
        SelectionRectangle = ResolveNegativeSpaceInRectangle(SelectionRectangle);
        var units = GameObject.FindGameObjectsWithTag("Marine");
        SelectedActors.Clear();
        foreach (GameObject unit in units)
        {
            Vector2 pos = unit.transform.position;
            if (SelectionRectangle.Contains(pos))
            {
                SelectedActors.Add(unit.GetComponent<Actor>());
            }
        }
        SelectionRectangle = new Rect(0, 0, 0, 0);

        foreach (Actor a in SelectedActors)
        {
            a.OnSelect();
        }
    }

    private Rect ResolveNegativeSpaceInRectangle(Rect rectToFix)
    {
        var newRect = new Rect();
        if (rectToFix.width < 0 && rectToFix.height > 0)
        {
            newRect.Set(rectToFix.xMax, rectToFix.yMin, -rectToFix.width, rectToFix.height);
        }
        else if (rectToFix.width > 0 && rectToFix.height < 0)
        {
            newRect.Set(rectToFix.xMin, rectToFix.yMax, rectToFix.width, -rectToFix.height);
        }
        else if (rectToFix.width < 0 && rectToFix.height < 0)
        {
            newRect.Set(rectToFix.xMax, rectToFix.yMax, -rectToFix.width, -rectToFix.height);
        }
        return newRect;
    }

    private Rect ConvertRectToScreen(Rect subject)
    {
        subject = ResolveNegativeSpaceInRectangle(subject);
        var min = Camera.main.WorldToScreenPoint(subject.position);
        var result = new Rect(min, subject.size * Camera.main.orthographicSize);
        return result;
    }

    private Vector2 CalculatePivotForRect(Rect rectForPivot)
    {
        if (rectForPivot.width < 0 && rectForPivot.height > 0)
        {
            return new Vector2(1, 0);
        }
        else if (rectForPivot.width > 0 && rectForPivot.height < 0)
        {
            return new Vector2(0, 1);
        }
        else if (rectForPivot.width < 0 && rectForPivot.height < 0)
        {
            return Vector2.one;
        }
        else
        {
            return Vector2.zero;
        }
    }

    private void Deselect()
    {
        foreach (Actor a in SelectedActors)
        {
            a.OnDeselect();
        }
        SelectedActors.Clear();
        UIEventHub.Instance.RaiseOnSelectionChanged(SelectedActors);
    }

    private void HandleBlueprintLeftClick()
    {
        var validPlacement = Map.ValidateBuildingPlacement(SelectedBlueprint, transform.position);
        if (!validPlacement)
        {
            Debug.Log("Can't build there.");
            return;
        }

        var result = BuildingFactory.Instance.PlaceBlueprint(transform.position);
        Map.Buildings.Add(result);
        
        CurrentState = CursorState.NORMAL;
        UIEventHub.Instance.RaiseOnBlueprintSelected(null);
    }

    private void HandleBlueprintRightClick()
    {
        CurrentState = CursorState.NORMAL;
        UIEventHub.Instance.RaiseOnBlueprintSelected(null);
    }
}
