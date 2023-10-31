using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Rigidbody2D rb;
    
    public float chance;
    public int cost;
    public Sprite sprite;


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
    public void Initialize(ItemData itemData)
    {
        name = itemData.name;
        chance = itemData.chance;
        cost = itemData.cost;
        sprite = itemData.sprite;

        // Thiết lập sprite cho đối tượng Item
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && sprite != null)
        {
            spriteRenderer.sprite = sprite;
        }
    }
}
