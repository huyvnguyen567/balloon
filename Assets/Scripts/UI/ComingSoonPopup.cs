using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ComingSoonPopup : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private RectTransform backgroundText;
    private void OnEnable()
    {
        ShowPopup();
    }

    private void ShowPopup()
    {
        gameObject.SetActive(true);
        StartCoroutine(HidePopupAfterDelay(2.0f));
        AnimatePopupIn();
    }

    private void AnimatePopupIn()
    {
        backgroundText.localScale = Vector3.one;
        backgroundText.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.2f).From();
    }

    private IEnumerator HidePopupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        HidePopup();
    }

    private void HidePopup()
    {
        gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        HidePopup();
    }
}
