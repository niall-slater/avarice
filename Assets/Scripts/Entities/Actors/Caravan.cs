using Assets.Scripts.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Caravan : MovingUnit
{
    public float CollectedOre;

    public float CollectionRate = 50f;

    public float CollectionRange = 0.5f;

    private List<Mine> _itinerary;

    private Mine _currentMine;

    public Rigidbody2D Body;

    public float MoveForce;

    public enum JourneyState
    {
        MAKING_ROUNDS,
        GOING_HOME
    }

    public JourneyState CurrentState;

    protected override void Start()
    {
        base.Start();

        ActorName = NameGenerator.GenerateName();

        CurrentState = JourneyState.MAKING_ROUNDS;
        _itinerary = Map.GetMines();

        if (_itinerary.Count >= 2)
        {
            // Sort itinerary into closest mine first
            _itinerary = _itinerary.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).ToList();
        }

        if (_itinerary.Count >= 1)
        {
            _currentMine = _itinerary[0];
            _moveTarget = _currentMine.transform.position;
        }

        PopUpManager.CreatePopup("CARAVAN ARRIVED");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Hurt(1f, null);
            return;
        }

        var bounceForce = Body.mass;
        var bounceBack = -(collision.transform.position - transform.position).normalized * bounceForce;
        Body.AddForce(bounceBack, ForceMode2D.Impulse);
    }

    protected void Update()
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

                    if (Vector3.Distance(transform.position, _moveTarget) < CollectionRange)
                    {
                        var amountToCollect = CollectionRate * Time.deltaTime;
                        var collection = _currentMine.CollectOre(amountToCollect);
                        CollectedOre += collection;
                        if (collection < amountToCollect)
                        {
                            NextMine();
                            return;
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
                    if (Vector3.Distance(transform.position, _moveTarget) < CollectionRange)
                    {
                        GameController.Cash += CollectedOre;
                        Debug.Log("Caravan safely left the map");
                        Destroy(gameObject);
                    }
                    break;
                }
        }
    }

    protected override void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, _moveTarget) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _moveTarget, MoveSpeed * Time.deltaTime);
            var moveForce = (_moveTarget - transform.position).normalized * MoveForce;

            var moveForce2D = new Vector3(moveForce.x, moveForce.y);
            Body.AddForce(moveForce2D, ForceMode2D.Force);
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
        _currentMine = _itinerary[0];
        _moveTarget = _currentMine.transform.position;
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
