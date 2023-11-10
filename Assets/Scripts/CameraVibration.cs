using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVibration : MonoBehaviour
{
    public static CameraVibration Instance;
    private Vector3 originalPosition;
    private float vibrationDuration = 0.2f; // Thời gian rung
    private float vibrationMagnitude = 0.03f; // Mức độ rung

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        originalPosition = new Vector3(0, 0, -10);
    }

    // Gọi hàm này để làm rung camera
    public void VibrateCamera()
    {
        StartCoroutine(DoCameraVibration());
    }

    private IEnumerator DoCameraVibration()
    {
        float elapsed = 0f;
        while (elapsed < vibrationDuration)
        {
            // Tạo một lượng rung ngẫu nhiên dựa trên vibrationMagnitude
            Vector3 randomVibration = Random.insideUnitSphere * vibrationMagnitude;
            transform.position = originalPosition + randomVibration;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
    }
}
