using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using System;

public class Ball : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] public float size;
    [SerializeField] protected float jumpForce;

    [SerializeField] protected TextMeshPro healthText;
    [SerializeField] protected float minDelayShowing = 1f;
    [SerializeField] protected float maxDelayShowing = 2f;
    [SerializeField] private List<Color> ballColors = new List<Color>();

    protected int[] leftAndRight = new int[] { -1, 1 };
    protected Rigidbody2D rb;
    protected bool isShowing;
    protected int minSize = 1;
    private SpriteRenderer spriteRenderer;

    [HideInInspector] public bool isResultOfFission = true;

    public float initHealth;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
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
            rb.AddTorque(rb.angularVelocity);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health > 0)
        {
            UpdateVisuals();
        }
        else
        {
            Die();
            if(size == 1)
            {
                DropItem();
            }
        }
        
    }

    protected void UpdateVisuals()
    {
        if (health < 1)
        {
            health = 1;
        }
        healthText.text = ((int)health).ToString();

        spriteRenderer.color = GetHealthColor(GameController.Instance.CurrentLevel,(int)health);
        transform.localScale = new Vector2(size, size);
    }

    public Color GetHealthColor(int level, int health)
    {
        int colorsCount = ballColors.Count;
        int colorIndex;
        if(health < 10)
        {
            colorIndex = health - 1;
        }
        else
        {
            // Tính toán chỉ số màu dựa trên số lượng máu và level
            colorIndex = (health - 1) / (level);
            if (colorIndex > colorsCount - 1)
            {
                colorIndex = colorIndex % colorsCount;
            }
        } 
        return ballColors[colorIndex];
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public void DropItem()
    {
        float random = Random.value;
        if (random > 0.4f)
        {
            GameObject item;
            item = Instantiate(GameController.Instance.GetRandomItem(), transform.position, Quaternion.identity);
        }      
    }

}
