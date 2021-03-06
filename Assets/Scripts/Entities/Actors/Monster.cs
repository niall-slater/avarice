using Assets.Scripts.Helpers;
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

    private float _crawlingTicker = .5f;

    public Rigidbody2D Body;

    public SpriteRenderer spriteRenderer;

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
    private GameObject _target;

    /// <summary>
    /// The position this actor is trying to move to.
    /// </summary>
    private Vector3 _moveTarget;


    //deprecated
    public float AttackPower = 1f;
    public float AttackCooldown = 1f;
    private float _attackCooldownTicker;

    private float _findTargetInterval = 1f;
    private float _findTargetTicker;

    public float WiggleMagnitude = 3f;
    private Vector3 _wiggle;
    public float WiggleInterval = 1f;
    private float _wiggleTicker;

    float _lifeTicker;

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

    public void Reinitialise(Vector3 startPosition, bool isGiant = false)
    {
        transform.position = startPosition;
        gameObject.SetActive(true);
        KillCount = 0;

        HP = isGiant ? GameVariables.GIANT_MONSTER_HP : GameVariables.DEFAULT_MONSTER_HP;

        var target = Map.GetRandomNonMineBuildingWithinRange(transform.position, AttackRadius);
        if (target == null)
        {
            CurrentBehaviour = Behaviour.WANDER;
            _moveTarget = GetRandomWanderTarget();
        }
        else
        {
            CurrentBehaviour = Behaviour.ATTACK;
            _target = target.gameObject;
        }

        ActorName = NameGenerator.GenerateName();

        ActorEventHub.Instance.RaiseOnMonsterSpawned(this);
        GameController.RefreshMonsterCount();
        ScoreEventHub.Instance.OnBioBombDetonation += Kill;
        _lifeTicker = 0f;
    }

    void Update()
    {
        _lifeTicker += Time.deltaTime;
        if (_crawlingTicker >= 0f)
        {
            _crawlingTicker -= Time.deltaTime;
            spriteRenderer.sortingOrder = -500;
            return;
        }
        else
        {
            _crawlingTicker = 0;
            spriteRenderer.sortingOrder = 0;
        }

        _wiggleTicker -= Time.deltaTime;

        if (_wiggleTicker < 0)
        {
            _wiggle = UnityEngine.Random.insideUnitSphere * WiggleMagnitude;
            _wiggleTicker = WiggleInterval;
        }

        if (_attackCooldownTicker > 0)
            _attackCooldownTicker -= Time.deltaTime;

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

    private void FixedUpdate()
    {
        if (_crawlingTicker > 0f)
        {
            return;
        }
        var moveForce = (_moveTarget - transform.position).normalized * MoveForce;

        var moveForce2D = new Vector3(moveForce.x, moveForce.y);
        Body.AddForce(moveForce2D, ForceMode2D.Force);
    }

    private void UpdateAttack()
    {
        if (_target == null)
        {
            _currentBehaviour = Behaviour.WANDER;
            return;
        }
        _moveTarget = _target.transform.position + _wiggle;
        _moveTarget.z = 0f;
    }

    private void UpdateWander()
    {
        if (Vector3.Distance(transform.position, _moveTarget) < .5f)
        {
            GetRandomWanderTarget();
        }
        
        if (Mathf.RoundToInt(_lifeTicker) % 5 == 0)
        {
            var enemy = GameObject.FindGameObjectWithTag("Marine");
            if (enemy == null)
            {
                enemy = GameObject.FindGameObjectWithTag("Building");
            }
            if (enemy == null)
            {
                enemy = GameObject.FindGameObjectWithTag("Builder");
            }

            _target = enemy;
            _currentBehaviour = Behaviour.ATTACK;
        }
    }

    private Vector3 GetRandomWanderTarget()
    {
        var target = transform.position + Random.insideUnitSphere * WanderRadius;
        target.z = 0;
        return target;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_target != null)
            return;

        if (collider.gameObject.layer != LayerMask.NameToLayer("Bullets"))
        {
            _target = collider.gameObject;
            CurrentBehaviour = Behaviour.ATTACK;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Hurt(GameVariables.BULLET_DAMAGE, null);
            return;
        }

        if (collision.gameObject.CompareTag("Caravan"))
        {
            Kill(null);
            return;
        }

        var bounceForce = Body.mass * 2f;
        var bounceBack = -(collision.transform.position - transform.position).normalized * bounceForce;
        Body.AddForce(bounceBack, ForceMode2D.Impulse);
    }

    protected override void Kill(Actor killer)
    {
        gameObject.SetActive(false);
        HP = 0f;
        ActorEventHub.Instance.RaiseOnMonsterKilled(this);
        ActorEventHub.Instance.RaiseOnActorDestroyed(this, killer);
        ScoreEventHub.Instance.OnBioBombDetonation -= Kill;
        GameController.RefreshMonsterCount();
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
