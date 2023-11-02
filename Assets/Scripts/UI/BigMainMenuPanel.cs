using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BigMainMenuPanel : MonoBehaviour
{
    //Script 
    public BattleTutorialPanel battleTutorialPanelScript;
    //GameObject con
    public GameObject themePanel;
    public GameObject topPanel;
    public GameObject bottomPanel;
    public GameObject rightPanel;
    public GameObject battleTutorialPanel;
    public GameObject cannonPanel;
    public GameObject shopPanel;

    //Bottom Panel
    [SerializeField] private Button themeButton;
    [SerializeField] private Button cannonButton;
    [SerializeField] private Button battleButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button lockButton;

    //private List<Button> buttons = new List<Button>();
    private Color defaultColor = new Color(0.11f, 0.22f, 0.34f, 1f);
    private Color selectedColor = new Color(0.8f, 0.7f, 0.4f, 1f);
    private Button currentSelectedButton;

    //Top Panel
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text diamondText;
    private void OnEnable()
    {
        UpdateCoinText();
        UpdateDiamondText();
        themePanel.SetActive(false);
        cannonPanel.SetActive(false);
        shopPanel.SetActive(false);
        OnClickChangeColorEvent(themeButton);
        OnClickChangeColorEvent(cannonButton);
        OnClickChangeColorEvent(battleButton);
        OnClickChangeColorEvent(shopButton);
        OnButtonClick(battleButton);
        themeButton.onClick.AddListener(() => OnThemeEventClick());
        battleButton.onClick.AddListener(() => OnBattleEventClick());
        cannonButton.onClick.AddListener(() => OnCannonEventClick());
        shopButton.onClick.AddListener(() => OnShopEventClick());
        lockButton.onClick.AddListener(() => OnLockClick());

        GameController.Instance.onStartGame.AddListener(delegate { 
            bottomPanel.SetActive(false);
            battleTutorialPanel.SetActive(false);
        });

    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        OnPlayEventClick();
    //    }
    //}
    private void Update()
    {
        if (!battleTutorialPanel.activeInHierarchy)
        {
            UIManager.Instance.menuContentPopup.SetActive(false);
        }
        else
        {
            UIManager.Instance.menuContentPopup.SetActive(true);
        }
    }

    public void UpdateCoinText()
    {
        //coinText.text = $"{DataManager.Instance.coin}";
        FormatAndSetNumber(coinText, DataManager.Instance.coin);
    }

    public void UpdateDiamondText()
    {
        //diamondText.text = $"{DataManager.Instance.diamond}";
        FormatAndSetNumber(diamondText, DataManager.Instance.diamond);
    }

    public void FormatAndSetNumber(TMP_Text textField, int number)
    {
        string formattedNumber;

        if (number < 1000)
        {
            formattedNumber = number.ToString();
        }
        else if (number < 1000000)
        {
            formattedNumber = (number / 1000f).ToString("F2") + "k";
        }
        else
        {
            formattedNumber = (number / 1000000f).ToString("F5") + "M";
        }

        textField.text = formattedNumber;
    }
    public void OnClickChangeColorEvent(Button button)
    {
        button.GetComponent<Image>().color = defaultColor;
        button.onClick.AddListener(() => OnButtonClick(button));
    }
    private void OnButtonClick(Button clickedButton)
    {
        if (currentSelectedButton != null)
        {
            currentSelectedButton.GetComponent<Image>().color = defaultColor;
        }

        currentSelectedButton = clickedButton;
        currentSelectedButton.GetComponent<Image>().color = selectedColor;
    }

    public void OnThemeEventClick()
    {
        themePanel.SetActive(true);
        battleTutorialPanel.SetActive(false);
        rightPanel.SetActive(false);
        cannonPanel.SetActive(false);
        shopPanel.SetActive(false);
    }

    public void OnBattleEventClick()
    {
        battleTutorialPanel.SetActive(true);
        rightPanel.SetActive(true);
        themePanel.SetActive(false);
        cannonPanel.SetActive(false);
        shopPanel.SetActive(false);
    }

    public void OnCannonEventClick()
    {
        cannonPanel.SetActive(true);
        themePanel.SetActive(false);
        battleTutorialPanel.SetActive(false);
        rightPanel.SetActive(false);
        shopPanel.SetActive(false);
    }

    public void OnShopEventClick()
    {
        shopPanel.SetActive(true);
        cannonPanel.SetActive(false);
        themePanel.SetActive(false);
        battleTutorialPanel.SetActive(false);
        rightPanel.SetActive(false);
    }
    public void OnLockClick()
    {
        UIManager.Instance.comingSoonPopup.SetActive(true);
    }
    //public void OnPlayEventClick()
    //{
    //    if(battleTutorialPanelScript.hasBeenClicked && GameController.Instance.currentGameState == GameController.GameState.MainMenu)
    //    {
    //        GameController.Instance.onStartGame.Invoke();
    //        Instantiate(GameController.Instance.cannonPrefab, battleTutorialPanelScript.cannon.transform.position, Quaternion.identity);
    //        battleTutorialPanelScript.hasBeenClicked = false;
    //        bottomPanel.SetActive(false);
    //        GameController.Instance.SwitchGameState(GameController.GameState.Gameplay);
    //    }
    //    else
    //    {
    //        battleTutorialPanelScript.hasBeenClicked = false;
    //    }
    //}
}
