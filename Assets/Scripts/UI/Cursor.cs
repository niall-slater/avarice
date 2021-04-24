using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public LayerMask SelectionMask;

    public Actor SelectedActor;

    public GameObject SelectedBlueprint;

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
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        if (Input.GetMouseButtonDown(0))
        {
            switch (CurrentState)
            {
                case CursorState.NORMAL:
                    HandleNormalLeftClick();
                    break;
                case CursorState.BLUEPRINT:
                    HandleBlueprintLeftClick();
                    break;
            }
        }

        if (Input.GetMouseButtonDown(1))
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

    private void HandleBlueprintRightClick()
    {
        CurrentState = CursorState.NORMAL;
        //Clear current blueprint
    }

    private void HandleNormalRightClick()
    {

        if (SelectedActor == null)
            return;

        //TODO: give different orders depending on where we clicked
        //var hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.zero, 1f, SelectionMask, 0f);
        //var actor = hit.collider.GetComponent<Actor>();

        SelectedActor.GiveRightClickOrder(transform.position);
    }

    private void HandleNormalLeftClick()
    {
        var cachedSetting = Physics2D.queriesHitTriggers;
        Physics2D.queriesHitTriggers = false;
        var hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.zero, 1f, SelectionMask, 0f);
        Physics2D.queriesHitTriggers = cachedSetting;

        if (!hit)
        {
            return;
        }
        var actor = hit.collider.GetComponent<Actor>();

        if (actor == SelectedActor)
        {
            return;
        }

        if (SelectedActor != null)
        {
            SelectedActor.OnDeselect();
            SelectedActor = null;
        }

        if (actor == null)
        {
            // Deselecting actor
            UIEventHub.Instance.RaiseOnSelectionChanged(SelectedActor);
            return;
        }

        // Selecting actor
        UIEventHub.Instance.RaiseOnSelectionChanged(SelectedActor);
        SelectedActor = actor;
        actor.OnSelect();
    }

    private void HandleBlueprintLeftClick()
    {
        
    }
}
