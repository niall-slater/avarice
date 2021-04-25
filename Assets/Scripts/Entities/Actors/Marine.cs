using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marine : MovingUnit
{
    public float FireInterval;

    public LayerMask LineOfSightMask;
    
    private float _fireTicker;

    private GameObject _target;

    [SerializeField]
    private AudioSource _audio;
    [SerializeField]
    private AudioClip _clipShoot;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ActorName = "PVT. " + NameGenerator.GenerateName();
        _fireTicker = FireInterval + Random.Range(0, FireInterval);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_target != null)
            return;

        if (collision.gameObject.CompareTag("Monster"))
        {
            _target = collision.gameObject;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            if (collision.gameObject.name == "GiantMonster(Clone)")
            {
                Hurt(GameVariables.GIANT_MONSTER_ATTACKPOWER, null);
            }
            else
            {
                Hurt(GameVariables.DEFAULT_MONSTER_ATTACKPOWER, null);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        _fireTicker -= Time.deltaTime;
        if (_fireTicker < 0)
        {
            if (_target != null && _target.activeSelf && !Physics2D.Linecast(transform.position, _target.transform.position, LineOfSightMask))
            {
                FireAt(_target);
            }
            else
            {
                _target = null;
            }
            _fireTicker = FireInterval;
        }
    }

    private void FireAt(GameObject target)
    {
        SpawnBullet((target.transform.position - transform.position).normalized);
    }

    public void SpawnBullet(Vector3 direction)
    {
        if (GameController.SpawnBullet(transform.position, direction, this))
        {
            _audio.pitch = UnityEngine.Random.Range(0.5f, .8f);
            _audio.PlayOneShot(_clipShoot);
        }
    }
}
