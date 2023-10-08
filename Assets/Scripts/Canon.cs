using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float rotateWheelSpeed = 4000f;

    [SerializeField] private Transform leftWheel;
    [SerializeField] private Transform rightWheel;

    [SerializeField] private float fireRate = 0.2f;
    private float fireTimer = 0;

    private bool isMoving = false;

    private Camera cam;
    private Rigidbody2D rb;
    private Vector2 pos;

    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        pos = rb.position;
    }
    void Update()
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
            RotateWheel(mouseX);

            fireTimer += Time.deltaTime;
            if (fireTimer >= fireRate)
            {
                Fire();
                fireTimer = 0;
            }

            float currentX = transform.position.x;
            float minX = - GameController.Instance.ScreenWidth + GetComponent<Collider2D>().bounds.size.x / 2;
            float maxX = GameController.Instance.ScreenWidth - GetComponent<Collider2D>().bounds.size.x / 2;
            currentX = Mathf.Clamp(currentX, minX, maxX);
            transform.position = new Vector2(currentX, transform.position.y);
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            rb.MovePosition(Vector2.Lerp(rb.position, pos, moveSpeed * Time.fixedDeltaTime));
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
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
            ObjectPool.Instance.ReturnObjectToPool("Item", collision.gameObject);
        }
    }
}
