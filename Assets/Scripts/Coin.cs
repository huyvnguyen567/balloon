using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        float posX = transform.position.x;
        if (posX < 0)
        {
            rb.AddForce(Vector2.right * 10f + Vector2.up * 50f);
        }
        else
        {
            rb.AddForce(Vector2.left * 10f + Vector2.up * 50f);
        }
        rb.AddTorque(posX * 4f);
    }
}
