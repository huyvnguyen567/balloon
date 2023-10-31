using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CannonChangePanel : MonoBehaviour
{
    private List<CanonButtonSO> cannons;
    [SerializeField] private GameObject buyButtonPrefab;
    [SerializeField] private Transform content;
    public List<Button> buttonsList;
    private List<bool> isPurchasedStates = new List<bool>();

    private void OnEnable()
    {
        GameController.OnBuyClick.AddListener(() => UpdateCannonButton(UIManager.Instance.cannonBuyPopup.GetComponent<CannonBuyPopup>().index));
        cannons = DataManager.Instance.cannonsData;
    }
    private void Start()
    {
        for(int i = 0; i < cannons.Count; i++)
        {
            GameObject cannon = Instantiate(buyButtonPrefab, content.transform);
            Image cannonImage = cannon.transform.GetChild(0).GetComponent<Image>();
            Transform diamond = cannon.transform.GetChild(1);
            Image diamondImage = diamond.GetChild(0).GetComponent<Image>();
            TMP_Text diamonText = diamond.GetChild(1).GetComponent<TMP_Text>();
            int index = i;
            cannonImage.sprite = cannons[i].sprite;
            if (cannons[i].isPurchased)
            {
                cannonImage.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                diamond.gameObject.SetActive(false);
                cannon.GetComponent<Button>().onClick.AddListener(() => OnButtonIsPurchasedClick(index));
            }
            else
            {
                cannonImage.color = new Color(0.3f, 0.3f, 0.3f, 1f);
                diamonText.text = cannons[i].price.ToString();
                cannon.GetComponent<Button>().onClick.AddListener(()=> OnButtonIsNotPurchasedClick(index));

            }

            buttonsList.Add(cannon.GetComponent<Button>());
            // Xóa tất cả các hành động đã được gán trước đó cho nút
        }
    }

    
    public void OnButtonIsPurchasedClick(int index)
    {
        DataManager.Instance.cannonPrefab = cannons[index].prefab;
        DataManager.Instance.SaveCannonPrefab();
        Debug.Log("Đã mua");

    }

    public void OnButtonIsNotPurchasedClick(int index)
    {
        UIManager.Instance.cannonBuyPopup.SetActive(true);
        CannonBuyPopup cannonBuyPopup = UIManager.Instance.cannonBuyPopup.GetComponent<CannonBuyPopup>();
        cannonBuyPopup.cannonIcon.GetComponent<Image>().sprite = cannons[index].sprite;
        cannonBuyPopup.priceText.text = cannons[index].price.ToString();
        cannonBuyPopup.index = index;
    }

    public void UpdateCannonButton(int index)
    {
        Image cannonImage = buttonsList[index].transform.GetChild(0).GetComponent<Image>();
        Transform diamond = buttonsList[index].transform.GetChild(1);

        cannonImage.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        cannonImage.color = Color.white;
        diamond.gameObject.SetActive(false);
        buttonsList[index].onClick.RemoveAllListeners();
        buttonsList[index].onClick.AddListener(() => OnButtonIsPurchasedClick(index));
    }
    private void OnDisable()
    {
        GameController.OnBuyClick.RemoveListener(() => UpdateCannonButton(UIManager.Instance.cannonBuyPopup.GetComponent<CannonBuyPopup>().index));
    }
}
