using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using System;

public class Ball : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public float size;
    [SerializeField] protected float jumpForce;

    [SerializeField] protected TextMeshPro healthText;
    [SerializeField] protected float minDelayShowing = 1f;
    [SerializeField] protected float maxDelayShowing = 2f;
  
    protected int[] leftAndRight = new int[] { -1, 1 };
    protected Rigidbody2D rb;
    protected bool isShowing;
    [HideInInspector] public bool isResultOfFission = true;

    public int initHealth;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        initHealth = health;
        UpdateVisuals();

        isShowing = true;
        rb.gravityScale = 0f;
        if (isResultOfFission)
        {
            FallDown();
        }
        else
        {
            int direction = leftAndRight[Random.Range(0, 2)];
            float screenOffset = GameController.Instance.ScreenWidth * 1.3f;
            transform.position = new Vector2(screenOffset * direction, transform.position.y);
            rb.velocity = new Vector2(-direction, 0);
            Invoke("FallDown", Random.Range(minDelayShowing, maxDelayShowing));
        }
   
    }
    private void FallDown()
    {
        isShowing = false;
        rb.gravityScale = 1f;
        rb.AddTorque(Random.Range(-20, 20));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cannon"))
        {
            //game over
            Debug.Log("game over");
        }
        else if (!isShowing && collision.CompareTag("Wall"))
        {
            float posX = transform.position.x;
            if (posX < 0)
            {
                rb.AddForce(Vector2.right * 150f);
            }
            else
            {
                rb.AddForce(Vector2.left * 150f);
            }
            rb.AddTorque(posX * 4f);
        }
        else if (collision.CompareTag("Ground"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            rb.AddTorque(rb.angularVelocity * 4f);
        }
    }

    public void TakeDamage(int damage)
    {
        if (health > damage)
        {
            health -= damage;
            UpdateVisuals();
        }
        else
        {
            Die();
        }
        
    }

    protected void UpdateVisuals()
    {
        healthText.text = health.ToString();
        transform.localScale = new Vector2(size, size);
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
    //private void Split()
    //{
    //    if (size > 1)
    //    {
    //        GameObject smallBall1 = Instantiate(gameObject, transform.position, Quaternion.identity);
    //        GameObject smallBall2 = Instantiate(gameObject, transform.position, Quaternion.identity);

    //        Ball smallBallScript1 = GetComponent<Ball>();
    //        Ball smallBallScript2 = GetComponent<Ball>();

    //        smallBall1.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 7f);
    //        smallBall2.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 7f);

    //        smallBallScript1.size = size * 0.75f;
    //        smallBallScript2.size = size * 0.75f;

    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }

    //}
}
