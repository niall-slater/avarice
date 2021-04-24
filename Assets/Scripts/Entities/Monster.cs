using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Actor
{
    public enum Behaviour
    {
        WANDER,
        ATTACK
    }

    private Behaviour _currentBehaviour;

    public Behaviour CurrentBehaviour
    {
        get { return _currentBehaviour; }
        set { SetBehaviour(value);  }
    }

    public Rigidbody2D Body;

    /// <summary>
    /// How close to a building before this creature attacks it?
    /// </summary>
    public float AttackRadius = 5f;

    /// <summary>
    /// How far will this creature try to wander per move?
    /// </summary>
    public float WanderRadius = 15f;

    /// <summary>
    /// How much force does this creature use to move?
    /// </summary>
    public float MoveForce = 5f;

    /// <summary>
    /// The actor this creature wants to attack.
    /// </summary>
    private Actor _target;

    /// <summary>
    /// The position this actor is trying to move to.
    /// </summary>
    private Vector3 _moveTarget;

    private float HP = 5f;

    public bool Alive;

    /// <summary>
    /// One-off logic for starting a behaviour
    /// </summary>
    private void SetBehaviour(Behaviour newBehaviour)
    {
        _currentBehaviour = newBehaviour;

        switch (newBehaviour)
        {
            case Behaviour.WANDER:
                _moveTarget = GetRandomWanderTarget();
                break;
            case Behaviour.ATTACK:
                break;
            default:
                break;
        }
    }

    public void Reinitialise(Vector3 startPosition)
    {
        transform.position = startPosition;
        Alive = true;
        gameObject.SetActive(true);
        var target = Map.GetRandomBuildingWithinRange(transform.position, AttackRadius);
        if (target == null)
        {
            CurrentBehaviour = Behaviour.WANDER;
        }
        else
        {
            CurrentBehaviour = Behaviour.ATTACK;
            _target = target;
        }

        ActorEventHub.Instance.RaiseOnMonsterSpawned(this);
        GameController.RefreshMonsterCount();
    }

    void Update()
    {
        switch (_currentBehaviour)
        {
            case Behaviour.WANDER:
                UpdateWander();
                break;
            case Behaviour.ATTACK:
                UpdateAttack();
                break;
            default:
                break;
        }
    }

    public void Hurt(float amount)
    {
        HP -= amount;

        if (HP <= 0f)
        {
            Kill();
        }
    }

    private void Kill()
    {
        gameObject.SetActive(false);
        Alive = false;
        ActorEventHub.Instance.RaiseOnMonsterKilled(this);
        GameController.RefreshMonsterCount();
    }

    private void FixedUpdate()
    {
        var moveForce = (_moveTarget - transform.position).normalized * MoveForce;

        var moveForce2D = new Vector3(moveForce.x, moveForce.y);
        Body.AddForce(moveForce2D, ForceMode2D.Force);
    }

    private void UpdateAttack()
    {
        _moveTarget = _target.transform.position;
    }

    private void UpdateWander()
    {
        if (Vector3.Distance(transform.position, _moveTarget) < .1f)
        {
            GetRandomWanderTarget();
        }
    }

    private Vector3 GetRandomWanderTarget()
    {
        var target = transform.position + Random.insideUnitSphere * WanderRadius;
        target.z = 0;
        return target;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Building"))
        {
            _target = collision.gameObject.GetComponent<Building>();
            CurrentBehaviour = Behaviour.ATTACK;
        }
        if (collision.gameObject.CompareTag("Marine"))
        {
            _target = collision.gameObject.GetComponent<Marine>();
            CurrentBehaviour = Behaviour.ATTACK;
        }
    }

    public override void OnSelect()
    {
    }

    public override void OnDeselect()
    {
    }

    public override void GiveRightClickOrder(Vector3 clickPosition)
    {
    }
}
