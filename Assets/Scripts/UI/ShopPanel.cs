using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class ShopPanel : MonoBehaviour
{
    public Button freeBuy;
    public TMP_Text freeText;

    private UnityAction<bool> x;
    void Start()
    {
        freeText.text = $"Free {DataManager.Instance.freeCount}/1";
        freeBuy.onClick.AddListener(() => ShowAds());
    }

    public void ShowAds()
    {
        if (DataManager.Instance.freeCount >= 1) 
        {
            Debug.Log("show");
            ManagerAds.Ins.ShowRewardedVideo((x) =>
            {
                if (x)
                {
                    DataManager.Instance.diamond += 50;
                    DataManager.Instance.SaveDiamond();
                    UIManager.Instance.bigMainMenuPanel.GetComponent<BigMainMenuPanel>().UpdateDiamondText();
                    DataManager.Instance.freeCount--;
                    freeText.text = $"Free {DataManager.Instance.freeCount}/1";
                    DataManager.Instance.SaveFreeBuyCount();
                }
            });
        }
    }
}
