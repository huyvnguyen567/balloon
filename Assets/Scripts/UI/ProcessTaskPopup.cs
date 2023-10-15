using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ProcessTaskPopup : BaseUI
{
    public RectTransform backgroundRect;
    public TMP_Text taskNameText;
    public TMP_Text processText;
    public Slider processSlider;
    public Button okButton;
    
    private void OnEnable()
    {
        okButton.onClick.AddListener(() => Hide());
        backgroundRect.localScale = Vector3.one;
        backgroundRect.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.2f).From();
    }
}

