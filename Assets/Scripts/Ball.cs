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
    [SerializeField] protected bool isShowing;
    protected int minSize = 1;
    private SpriteRenderer spriteRenderer;
    private Collider2D col2D;

    public bool isResultOfFission = true;

    public float initHealth;
    public int initSize;
    protected bool isDead = false;


    public static UnityEvent UpdateScoreEvent = new UnityEvent();
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        col2D = GetComponent<Collider2D>();
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
            GameObject boss = GameObject.FindGameObjectWithTag("Boss");
            if (boss != null)
            {
                // Nếu "boss" tồn tại, đặt vị trí của quả bóng dựa trên "boss".
                transform.position = boss.transform.position;
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
            VibrationController.Vibrate();
            CameraVibration.Instance.VibrateCamera();
        }
        else if (!isShowing && collision.CompareTag("Wall"))
        {
            float wallDirection = Mathf.Sign(collision.transform.position.x - transform.position.x);
            if (wallDirection < 0)
            {
                // Bóng va chạm với tường bên trái
                rb.velocity = new Vector2(2, rb.velocity.y);
            }
            else
            {
                // Bóng va chạm với tường bên phải
                rb.velocity = new Vector2(-2, rb.velocity.y);
            }
            VibrationController.Vibrate();
            CameraVibration.Instance.VibrateCamera();
        }
        else if (collision.CompareTag("Ground"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            rb.AddTorque(rb.angularVelocity);
            VibrationController.Vibrate();
            CameraVibration.Instance.VibrateCamera();
        }
        else if (collision.CompareTag("Top"))
        {
            rb.AddForce(Vector2.up);
        }
    }

 
    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            health -= damage;
            DataManager.Instance.previousScore = DataManager.Instance.score;
            DataManager.Instance.score += damage;
            DataManager.Instance.SaveHighScore();
            DataManager.Instance.SaveTaskTypeData(TaskType.PointsInOneGame, (int)DataManager.Instance.highScore);
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
        isDead = true;
        ObjectPool.Instance.ReturnObjectToPool("Ball", gameObject);
    }

}
