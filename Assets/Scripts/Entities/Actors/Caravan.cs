using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Caravan : MovingUnit
{
    public float CollectedOre;

    public float CollectionRate = 50f;

    private List<Mine> _itinerary;

    private Mine _currentMine;

    public enum JourneyState
    {
        MAKING_ROUNDS,
        GOING_HOME
    }

    public JourneyState CurrentState;

    protected override void Start()
    {
        base.Start();
        CurrentState = JourneyState.MAKING_ROUNDS;
        _itinerary = Map.GetMines();

        if (_itinerary.Count >= 2)
        {
            // Sort itinerary into closest mine first
            _itinerary = _itinerary.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).ToList();
        }

    }

    private void Update()
    {
        switch (CurrentState)
        {
            case JourneyState.MAKING_ROUNDS:
                {
                    if (_itinerary.Count == 0)
                    {
                        FinishRounds();
                        return;
                    }

                    if (_currentMine == null || !_currentMine.Alive)
                    {
                        NextMine();
                        return;
                    }

                    if (transform.position == _moveTarget)
                    {
                        var amountToCollect = CollectionRate * Time.deltaTime;
                        var collection = _currentMine.CollectOre(amountToCollect);
                        CollectedOre += collection;
                        if (collection < amountToCollect)
                        {
                            NextMine();
                        }
                    }

                    if (_itinerary.Count == 0)
                    {
                        CurrentState = JourneyState.GOING_HOME;
                    }
                    break;
                }
            case JourneyState.GOING_HOME:
                {
                    if (transform.position == _moveTarget)
                    {
                        GameController.Cash += CollectedOre;
                        Debug.Log("Caravan safely left the map");
                        Destroy(gameObject);
                    }
                    break;
                }
        }
    }

    private void NextMine()
    {
        _itinerary.RemoveAt(0);
        if (_itinerary.Count == 0)
        {
            FinishRounds();
            return;
        }
    }

    private void FinishRounds()
    {
        CurrentState = JourneyState.GOING_HOME;
        _currentMine = null;
        _moveTarget = Map.GetEgress();
    }

    public override void GiveRightClickOrder(Vector3 clickPosition)
    {
    }

    public override void OnDeselect()
    {
    }

    public override void OnSelect()
    {
    }
}
