using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleTutorialPanel : BaseUI, IPointerDownHandler
{
    public GameObject cannon;
    public GameObject mouseHighLight;
    public bool hasBeenClicked = false; // Biến để kiểm tra đã nhấp chuột lần đầu hay chưa
    private bool isCannonCreated = false;
    public TMP_Text levelText;
    public TMP_Text highScoreText;

    public Button[] upgradeButtons;
    public GameObject upgradePanel;

    public Button upgradeFireSpeed;
    public TMP_Text fireSpeedText;
    public TMP_Text fireSpeedCostText;
    private float fireRate;

    public Button upgradeFirePower;
    public TMP_Text firePowerText;
    public TMP_Text firePowerCostText;


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
        }

        DataManager.Instance.LoadUpgradeData();
        GameController.Instance.onStartGame.AddListener(Hide);

        for(int i = 0; i< upgradeButtons.Length; i++)
        {
            int buttonIndex = i;
            upgradeButtons[i].onClick.AddListener(() => ActivateContent(buttonIndex));
        }

        upgradeFireSpeed.onClick.AddListener(() => UpgradeFireSpeed());
        upgradeFirePower.onClick.AddListener(() => UpgradeFirePower());

        UpdateFireSpeedUI();
        UpdateFirePowerUI();
    }
    private void Start()
    {
    }
    private void Update()
    {
        if (cannonClone != null)
        {
            Vector3 newPosition = cannonClone.transform.position;
            newPosition.x = mouseHighLight.transform.position.x;
            cannonClone.transform.position = newPosition;
        }
      
    }

    public void UpgradeFireSpeed()
    {
        if (DataManager.Instance.coin >= DataManager.Instance.upgradeFireSpeedCost)
        {
            DataManager.Instance.coin -= DataManager.Instance.upgradeFireSpeedCost;
            DataManager.Instance.fireBulletSpeed += 0.3f;
            fireRate += 1.5f;
            DataManager.Instance.fireRateTime = 1 / fireRate;
            DataManager.Instance.upgradeFireSpeedCost += 3;
            UpdateFireSpeedUI();
            DataManager.Instance.SaveUpgradeData();
        }
        else
        {
            Debug.Log("Không đủ tiền");
            // Hiển thị thông báo rằng bạn không có đủ coin để nâng cấp
        }
    }

    private void UpdateFireSpeedUI()
    {
        fireRate = 1 / DataManager.Instance.fireRateTime;
        float dps = DataManager.Instance.fireDamage * fireRate / (21 / DataManager.Instance.fireBulletSpeed);
        fireSpeedText.text = $"{Mathf.Round(dps * 10) / 10.0f} dps";
        fireSpeedCostText.text = $"{DataManager.Instance.upgradeFireSpeedCost}";
    }


    public void UpgradeFirePower()
    {
        if (DataManager.Instance.coin >= DataManager.Instance.upgradeFirePowerCost)
        {
            DataManager.Instance.coin -= DataManager.Instance.upgradeFirePowerCost;
            DataManager.Instance.fireDamage += 0.1f;
            DataManager.Instance.upgradeFirePowerCost += 3;
            UpdateFirePowerUI();
            UpdateFireSpeedUI();
            DataManager.Instance.SaveUpgradeData();

        }
        else
        {
            // Hiển thị thông báo rằng bạn không có đủ coin để nâng cấp
        }
    }

    private void UpdateFirePowerUI()
    {
        firePowerText.text = $"{DataManager.Instance.fireDamage * 100}%";
        firePowerCostText.text = $"{DataManager.Instance.upgradeFirePowerCost}";
    }
    public void ActivateContent(int contentIndex)
    {
        // Tắt tất cả nội dung
        foreach (Transform child in upgradePanel.transform)
        {
            child.gameObject.SetActive(false);
        }

        // Bật nội dung tương ứng
        upgradePanel.transform.GetChild(contentIndex).gameObject.SetActive(true);

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
