using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marine : MovingUnit
{
    public float FireInterval = .2f;

    private float _fireTicker;

    private Actor _target;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _fireTicker = FireInterval + Random.Range(0, FireInterval);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            _target = collision.gameObject.GetComponent<Actor>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        _fireTicker -= Time.deltaTime;
        if (_fireTicker < 0)
        {
            if (_target != null && _target.Alive)
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

    private void FireAt(Actor target)
    {
        SpawnBullet((target.transform.position - transform.position).normalized);
    }

    public void SpawnBullet(Vector3 direction)
    {
        GameController.SpawnBullet(transform.position, direction);
    }
}
