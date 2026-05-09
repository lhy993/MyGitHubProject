using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject PLAYER;
    public float Enemy_Dmg = 0;
    public float speed = 6f;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        if (PLAYER == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                PLAYER = p.gameObject;
        }
    }
    public void Init(Transform target, float dmg)
    {
        Enemy_Dmg = dmg;
        Vector2 center = target.GetComponent<Collider2D>().bounds.center;
        Vector2 dir = (center - (Vector2)transform.position).normalized;
        rb.velocity = dir * speed;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 180f);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PLAYER.GetComponent<Character>()?.TakeDamage(Enemy_Dmg);
        }
        Destroy(gameObject);
    }
}
