using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public LayerMask SelectionMask;

    public Actor SelectedActor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        if (Input.GetMouseButtonDown(0))
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

        if (Input.GetMouseButtonDown(1))
        {
            if (SelectedActor == null)
                return;

            //TODO: give different orders depending on where we clicked
            //var hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.zero, 1f, SelectionMask, 0f);
            //var actor = hit.collider.GetComponent<Actor>();

            SelectedActor.GiveRightClickOrder(transform.position);
        }
    }
}
