using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigMainMenuPanel : MonoBehaviour
{
    //Script
    public MainMenuPanel mainMenuPanelScript;
    //GameObject con
    public GameObject themePanel;
    public GameObject mainMenuPanel;
    public GameObject topPanel;
    public GameObject bottomPanel;
    public GameObject rightPanel;

    //Top Panel
    [SerializeField] private Button themeButton;
    [SerializeField] private Button cannonButton;
    [SerializeField] private Button battleButton;
    [SerializeField] private Button shopButton;
    private List<Button> buttons = new List<Button>();
    private Color defaultColor = new Color(0.11f, 0.22f, 0.34f, 1f);
    private Color selectedColor = new Color(0.8f, 0.7f, 0.4f, 1f);
    private Button currentSelectedButton;

    //mainmenu
    private void OnEnable()
    {
        themePanel.SetActive(false);
        OnClickChangeColorEvent(themeButton);
        OnClickChangeColorEvent(cannonButton);
        OnClickChangeColorEvent(battleButton);
        OnClickChangeColorEvent(shopButton);
        themeButton.onClick.AddListener(() => OnThemeEventClick());
        battleButton.onClick.AddListener(() => OnBattleEventClick());
    }

    public void OnClickChangeColorEvent(Button button)
    {
        button.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
        button.onClick.AddListener(() => OnButtonClick(button));
    }
    private void OnButtonClick(Button clickedButton)
    {
        if (currentSelectedButton != null)
        {
            currentSelectedButton.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
        }

        currentSelectedButton = clickedButton;
        currentSelectedButton.transform.GetChild(0).GetComponent<Image>().color = selectedColor;
    }

    public void OnThemeEventClick()
    {
        themePanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        rightPanel.SetActive(false);
    }

    public void OnBattleEventClick()
    {
        mainMenuPanel.SetActive(true);
        rightPanel.SetActive(true);
        themePanel.SetActive(false);
    }
}
