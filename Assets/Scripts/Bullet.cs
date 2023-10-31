using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float lifeTime = 1.5f;
    [SerializeField] private float damage = 1;

    private Rigidbody2D rb;

    private float _spawnTime;
    private void OnEnable()
    {
        _spawnTime = Time.time;
        damage = DataManager.Instance.fireDamage;
        moveSpeed = DataManager.Instance.fireBulletSpeed;        
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(0, moveSpeed);
        if (Time.time - _spawnTime > lifeTime)
        {
            ObjectPool.Instance.ReturnObjectToPool("Bullet", gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            if (ball != null)
            {
                ball.TakeDamage(damage);

            }
            ObjectPool.Instance.ReturnObjectToPool("Bullet", gameObject);
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            Boss boss = collision.gameObject.GetComponent<Boss>();
            if (boss != null)
            {
                boss.TakeDamage(damage);

            }
            ObjectPool.Instance.ReturnObjectToPool("Bullet", gameObject);
        }
    }
}
