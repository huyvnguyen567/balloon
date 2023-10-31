using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canon : MonoBehaviour
{
    public bool isAnim = false;
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float rotateWheelSpeed = 4000f;

    [SerializeField] private Transform leftWheel;
    [SerializeField] private Transform rightWheel;

    [SerializeField] HingeJoint2D[] wheels;
    JointMotor2D motor;
    public float velocityX;
    private float screenBounds;

    [SerializeField] private float fireRateTime = 0.2f;

    private float fireTimer = 0;

    private bool isMoving = true;

    private Camera cam;
    private Rigidbody2D rb;
    private Vector2 pos;

    private void OnEnable()
    {
        fireRateTime = DataManager.Instance.fireRateTime;
    }
    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        pos = rb.position;
        motor = wheels[0].motor;
        screenBounds = GameController.Instance.ScreenWidth - 0.56f;
    }
    void Update()
    {
        if(GameController.Instance.currentGameState == GameController.GameState.Gameplay)
        {
            if (!isAnim)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isMoving = true;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    isMoving = false;
                }

                if (isMoving)
                {
                    pos.x = cam.ScreenToWorldPoint(Input.mousePosition).x;

                    float mouseX = Input.GetAxis("Mouse X");
                    //RotateWheel(mouseX);

                    fireTimer += Time.deltaTime;
                    if (fireTimer >= fireRateTime)
                    {
                        Fire();
                        fireTimer = 0;
                    }

                    //float currentX = transform.position.x;
                    //float minX = -GameController.Instance.ScreenWidth + GetComponent<Collider2D>().bounds.size.x / 2;
                    //float maxX = GameController.Instance.ScreenWidth - GetComponent<Collider2D>().bounds.size.x / 2;
                    //currentX = Mathf.Clamp(currentX, minX, maxX);
                    //transform.position = new Vector2(currentX, transform.position.y);
                }
            }
            else
            {
                //float mouseX = UIManager.Instance.bigMainMenuPanel.GetComponent<BigMainMenuPanel>().battleTutorialPanelScript.cannon.transform.position.x;
                //RotateWheel((float)-mouseX / 50);
            }
        }
        else
        {
            GetComponent<Collider2D>().enabled = false;
        }


    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            Vector2 newPosition = Vector2.Lerp(rb.position, pos, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        // Tính toán vận tốc dựa trên sự thay đổi vị trí trong các frame
        Vector2 deltaPosition = (rb.position - pos) / Time.fixedDeltaTime;
        velocityX = deltaPosition.x;
        if (!isAnim)
        {
            velocityX = -velocityX;
        }
        if (Mathf.Abs(velocityX) > 0.0f && Mathf.Abs(rb.position.x) < screenBounds)
        {
            motor.motorSpeed = velocityX * 50f;
            MotorActivate(true);
        }
        else
        {
            motor.motorSpeed = 0f;
            MotorActivate(false);
        }

        // Cập nhật giá trị pos
        pos = rb.position;
    }

    void MotorActivate(bool isActive)
    {
        wheels[0].useMotor = isActive;
        wheels[1].useMotor = isActive;

        wheels[0].motor = motor;
        wheels[1].motor = motor;
    }

    public void RotateWheel(float rotationValue)
    {
        float wheelRotation = - rotationValue * rotateWheelSpeed * Time.deltaTime;
        leftWheel.Rotate(Vector3.forward * wheelRotation);
        rightWheel.Rotate(Vector3.forward * wheelRotation);
    }

    public void Fire()
    {
        GameObject bullet = ObjectPool.Instance.GetObjectFromPool("Bullet");
        if(bullet != null)
        {
            bullet.transform.position = transform.position + new Vector3(0, 1, 0);
            bullet.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            DataManager.Instance.coin += collision.gameObject.GetComponent<Item>().cost;
            UIManager.Instance.bigMainMenuPanel.GetComponent<BigMainMenuPanel>().UpdateCoinText();
            DataManager.Instance.SaveCoin();
            TextPopup.Create(collision.transform.position, $"${collision.gameObject.GetComponent<Item>().cost}");
            ObjectPool.Instance.ReturnObjectToPool("Item", collision.gameObject);
        }
    }
}
