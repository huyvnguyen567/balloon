using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    public Transform waypoints; // Mảng chứa các waypoint.
    public float moveSpeed = 5.0f; // Tốc độ di chuyển của boss.
    public float health;
    public float initHealth;
    public float bossDamageTaken;

    private int currentWaypointIndex = 0;
    private Transform currentWaypoint;

    public static UnityEvent UpdateProcessEvent = new UnityEvent();


    private void Start()
    {
        int currentLevel = DataManager.Instance.currentLevel;

        // Tính toán máu của "boss" dựa trên cấp độ.
        health = CalculateBossHealth(currentLevel);
        initHealth = health;
        if (waypoints.childCount > 0)
        {
            currentWaypoint = waypoints.GetChild(0);
        }

    }

    private void Update()
    {
        if (currentWaypoint != null)
        {
            // Di chuyển boss tới waypoint hiện tại.
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);

            // Kiểm tra xem boss đã đến gần đủ waypoint hiện tại chưa.
            if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.1f)
            {
                // Di chuyển đến waypoint tiếp theo nếu có.
                currentWaypointIndex++;
                if (currentWaypointIndex < waypoints.childCount)
                {
                    currentWaypoint = waypoints.GetChild(currentWaypointIndex);
                }
                else
                {
                    // Nếu đã đi qua tất cả các waypoint, có thể kết thúc hành động hoặc lặp lại quá trình.
                    currentWaypointIndex = 0;
                    currentWaypoint = waypoints.GetChild(currentWaypointIndex);
                }
            }
        }
    }

    private float CalculateBossHealth(int level)
    {
        // Tính toán máu của "boss" dựa trên cấp độ.
        // Ở đây, chúng ta sử dụng công thức tùy ý, ví dụ: máu tăng gấp đôi sau mỗi 5 cấp độ.
        int baseHealth = 40; // Máu cơ bản của "boss" ở level 1.
        int healthIncrement = 40; // Số máu tăng thêm sau mỗi 5 cấp độ.

        int levelThreshold = 5; // Ngưỡng cấp độ (ví dụ: 5, 10, 15, ...)

        int additionalHealth = (level / levelThreshold) * healthIncrement;

        return baseHealth + additionalHealth;
    }
    public void TakeDamage(float damage)
    {
        StartCoroutine(ChangeColorWhenHit());
        health -= damage;
        bossDamageTaken += damage;
        UpdateProcessEvent.Invoke();
        if (health <= 0)
        {
            Die();
        }
        
    }
    public void Die()
    {
        Destroy(gameObject);
    }

    IEnumerator ChangeColorWhenHit()
    {
        SpriteRenderer spriteRender = transform.GetChild(0).GetComponent<SpriteRenderer>();
        spriteRender.color = new Color(255, 10, 0, 255);
        yield return new WaitForSeconds(0.1f);
        spriteRender.color = new Color(255, 255, 255, 255);
    }
}
