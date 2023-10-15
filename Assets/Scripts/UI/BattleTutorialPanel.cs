using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleTutorialPanel : BaseUI, IPointerDownHandler
{
    public GameObject cannon;
    public GameObject mouseHighLight;
    public bool hasBeenClicked = false; // Biến để kiểm tra đã nhấp chuột lần đầu hay chưa
    private bool isCannonCreated = false;
    GameObject cannonClone;

    void OnEnable()
    {
        if (!isCannonCreated)
        {
            cannonClone = Instantiate(GameController.Instance.cannonPrefab, cannon.transform.position, Quaternion.identity);
            //cannonClone.transform.localScale = new Vector3(52, 52, 1);
            cannonClone.GetComponent<Canon>().isAnim = true;
            isCannonCreated = true;
        }
        GameController.Instance.onStartGame.AddListener(Hide);
       
    }
    private void Update()
    {
        Vector3 newPosition = cannonClone.transform.position;
        newPosition.x = mouseHighLight.transform.position.x;
        cannonClone.transform.position = newPosition;
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

}
