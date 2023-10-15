using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;

public class CannonBuyPopup : BaseUI
{
    [SerializeField] private RectTransform backgroundRect;
    public Image cannonIcon;
    public TMP_Text priceText;
    [SerializeField] private Button okButton;
    [SerializeField] private Button buyButton;
    public int index;



    private void OnEnable()
    {
        okButton.onClick.AddListener(() => Hide());
        buyButton.onClick.AddListener(() => OnClickBuyButton(index));
        backgroundRect.localScale = Vector3.one;
        backgroundRect.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.2f).From();
    }

    public void OnClickBuyButton(int index)
    {
        Hide();
        GameController.Instance.BuyCannon(index);
        Debug.Log(index);
    }

    private void OnDisable()
    {
        buyButton.onClick.RemoveAllListeners();
        okButton.onClick.RemoveAllListeners();
    }
}
