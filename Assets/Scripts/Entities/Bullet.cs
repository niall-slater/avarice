using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool Alive;

    public float Speed = 5f;

    public float Damage = 0.1f;

    private float Lifetime = 5f;

    private Vector3 _movement;

    public TrailRenderer trail;

    private Actor _shooter;


    public void Reinitialise(Vector3 position, Vector3 direction, Actor shooter)
    {
        gameObject.SetActive(true);
        Alive = true;
        _shooter = shooter;
        transform.position = position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.back);

        _movement = direction;
        GameController.RefreshBulletCount();
        Lifetime = 5f;
        trail.enabled = true;
    }

    private void Update()
    {
        Lifetime -= Time.deltaTime;

        if (Lifetime < 0)
        {
            Kill();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<Monster>().Hurt(Damage, _shooter);
        }

        Kill();
    }

    private void FixedUpdate()
    {
        transform.position += _movement * Speed * Time.deltaTime;
    }

    public void Kill()
    {
        Alive = false;
        trail.enabled = false;
        GameController.RefreshBulletCount();
        gameObject.SetActive(false);
    }
}
