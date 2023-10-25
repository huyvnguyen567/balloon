using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class MenuContentPopup : BaseUI, IPointerDownHandler
{
    public GameObject cannon;
    public GameObject mouseHighLight;
    public bool hasBeenClicked = false; // Biến để kiểm tra đã nhấp chuột lần đầu hay chưa
    private bool isCannonCreated = false;
    public TMP_Text levelText;
    public TMP_Text highScoreText;

    private GameObject cannonClone;

    void OnEnable()
    {
        if (!isCannonCreated)
        {
            cannonClone = Instantiate(GameController.Instance.cannonPrefab, cannon.transform.position, Quaternion.identity);
            //cannonClone.transform.localScale = new Vector3(52, 52, 1);
            cannonClone.GetComponent<Canon>().isAnim = true;
            isCannonCreated = true;
            highScoreText.text = $"Best score: {(int)DataManager.Instance.highScore}";
            levelText.text = $"Level {GameController.Instance.CurrentLevel}";

            GameController.Instance.onStartGame.AddListener(Hide);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnPlayEventClick();
        }
        
        if (cannonClone != null)
        {
            Vector3 newPosition = cannonClone.transform.position;
            newPosition.x = mouseHighLight.transform.position.x;
            cannonClone.transform.position = newPosition;
        }
    }

    private void OnDisable()
    {
        GameController.Instance.onStartGame.RemoveListener(Hide);
        Destroy(cannonClone);
        isCannonCreated = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        hasBeenClicked = true;
    }

    public void OnPlayEventClick()
    {
        if (hasBeenClicked && GameController.Instance.currentGameState == GameController.GameState.MainMenu)
        {
            GameController.Instance.onStartGame.Invoke();
            Instantiate(GameController.Instance.cannonPrefab, cannon.transform.position, Quaternion.identity);
            hasBeenClicked = false;
            GameController.Instance.SwitchGameState(GameController.GameState.Gameplay);
        }
        else
        {
            hasBeenClicked = false;
        }
    }
}
