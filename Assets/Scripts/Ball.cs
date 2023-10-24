using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] public int size;
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
    public int initSize;

    public static UnityEvent UpdateScoreEvent = new UnityEvent();
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        initHealth = health;
        initSize = size;
    }
    private void Start()
    {
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
            GameController.Instance.SwitchGameState(GameController.GameState.Lose);
            //Debug.Log("game over");
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
        else if (collision.CompareTag("Top"))
        {
            rb.AddForce(Vector2.up);
        }
    } 

    public void TakeDamage(float damage)
    {
        health -= damage;
        DataManager.Instance.previousScore = DataManager.Instance.score;
        DataManager.Instance.score += damage;
        DataManager.Instance.SaveHighScore();
        
        UpdateScoreEvent.Invoke();
        if (health < 1)
        {
            Die();
        }
        else
        {
            UpdateVisuals();
        }
    }

    protected void UpdateVisuals()
    {
        if(health < 1)
        {
            healthText.text = "1";
        }
        healthText.text = ((int)health).ToString();
        spriteRenderer.color = GetHealthColor(GameController.Instance.CurrentLevel,(int)health);
        transform.localScale = new Vector2(size, size);
    }

    public Color GetHealthColor(int level, int health)
    {
        int colorsCount = ballColors.Count;
        int colorIndex = 0;

        if (health > 1 && health < colorsCount + 1)
        {
            colorIndex = health - 1;
        }
        else if (health > colorsCount)
        {
            // Tính toán chỉ số màu dựa trên số lượng máu và level
            colorIndex = (health - 1) / level % colorsCount;
        }

        return ballColors[colorIndex];
    }

    protected virtual void Die()
    {
        ObjectPool.Instance.ReturnObjectToPool("Ball", gameObject);
    }

}
