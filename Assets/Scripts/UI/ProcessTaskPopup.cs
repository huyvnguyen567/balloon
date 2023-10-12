using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProcessTaskPopup : BaseUI
{
    public TMP_Text taskNameText;
    public TMP_Text processText;
    public Slider processSlider;
    public Button okButton;

    private void OnEnable()
    {
        okButton.onClick.AddListener(() => Hide());
    }

}

