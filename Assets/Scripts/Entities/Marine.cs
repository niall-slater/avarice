using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marine : Actor
{
    public float FireInterval = .2f;
    public float MoveSpeed = 3f;

    private float _fireTicker;

    public GameObject SelectionHalo;

    private Transform _target;

    private Vector3 _moveTarget;

    // Start is called before the first frame update
    void Start()
    {
        _fireTicker = FireInterval + Random.Range(0, FireInterval);
        _moveTarget = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            _target = collision.gameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _fireTicker -= Time.deltaTime;
        if (_fireTicker < 0)
        {
            if (_target != null)
                FireAt(_target);
            _fireTicker = FireInterval;
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, _moveTarget) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _moveTarget, MoveSpeed * Time.deltaTime);
        }
    }

    private void FireAt(Transform target)
    {
        SpawnBullet((target.position - transform.position).normalized);
    }

    public void SpawnBullet(Vector3 direction)
    {
        GameController.SpawnBullet(transform.position, direction);
    }

    public override void OnSelect()
    {
        SelectionHalo.SetActive(true);
    }

    public override void OnDeselect()
    {
        SelectionHalo.SetActive(false);
    }

    public override void GiveRightClickOrder(Vector3 clickPosition)
    {
        _moveTarget = clickPosition;
    }
}
