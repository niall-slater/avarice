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


    public void Reinitialise(Vector3 position, Vector3 direction)
    {
        gameObject.SetActive(true);
        Alive = true;
        transform.position = position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.back);

        _movement = direction;
        GameController.BulletCount++;
        Lifetime = 5f;
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
            collision.gameObject.GetComponent<Monster>().Hurt(Damage);
        }

        Kill();
    }

    private void FixedUpdate()
    {
        transform.position += _movement * Speed * Time.deltaTime;
    }

    public void Kill()
    {
        gameObject.SetActive(false);
        Alive = false;
        GameController.BulletCount--;
    }
}
