using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopup : MonoBehaviour
{
    private TMP_Text text;
    private float disappearTimer;
    private Color textColor;

    public static TextPopup Create(Vector3 positon, string text)
    {
        UIManager.Instance.textPopup = UIManager.Instance.Spawn(UIType.Popup, UIManager.Instance.textPopupPrefab);
        Transform textPopupTransform = UIManager.Instance.textPopup.transform;
        textPopupTransform.position = positon;
        TextPopup textPopup = textPopupTransform.GetComponent<TextPopup>();
        textPopup.Setup(text);
        return textPopup;
    }
    private void Awake()
    {
        text = transform.GetComponent<TMP_Text>();
    }
    void Update()
    {
        float moveYSpeed = 2f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 0.4f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            text.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public void Setup(string textInput)
    {
        textColor = text.color;
        text.text = textInput;
    }
}
