using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuPanel : BaseUI, IPointerDownHandler
{
    public GameObject cannonAnim;
    public bool hasBeenClicked = false; // Biến để kiểm tra đã nhấp chuột lần đầu hay chưa

    void OnEnable()
    {
        GameController.Instance.onStartGame.AddListener(Hide);
    }

    private void OnDisable()
    {
        GameController.Instance.onStartGame.RemoveListener(Hide);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        hasBeenClicked = true;
    }

}
