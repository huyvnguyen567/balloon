using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSides : MonoBehaviour
{
    [SerializeField] private BoxCollider2D leftWallCollider;
    [SerializeField] private BoxCollider2D rightWallCollider;

    private void Start()
    {
        float screenWidth = GameController.Instance.ScreenWidth;
        leftWallCollider.transform.position = new Vector3(-screenWidth - leftWallCollider.size.x / 2, 0, 0);
        rightWallCollider.transform.position = new Vector3(screenWidth + rightWallCollider.size.x / 2, 0, 0);
    }
}
