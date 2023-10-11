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
    public GameObject topPanel;
    public GameObject bottomPanel;
    public GameObject rightPanel;
    public GameObject mainMenuPanel;

    //Top Panel
    [SerializeField] private Button themeButton;
    [SerializeField] private Button cannonButton;
    [SerializeField] private Button battleButton;
    [SerializeField] private Button shopButton;
    private List<Button> buttons = new List<Button>();
    private Color defaultColor = new Color(0.11f, 0.22f, 0.34f, 1f);
    private Color selectedColor = new Color(0.8f, 0.7f, 0.4f, 1f);
    private Button currentSelectedButton;


    private void OnEnable()
    {
        themePanel.SetActive(false);
        OnClickChangeColorEvent(themeButton);
        OnClickChangeColorEvent(cannonButton);
        OnClickChangeColorEvent(battleButton);
        OnClickChangeColorEvent(shopButton);
        OnButtonClick(battleButton);
        themeButton.onClick.AddListener(() => OnThemeEventClick());
        battleButton.onClick.AddListener(() => OnBattleEventClick());
    }
   
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnPlayEventClick();
        }
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
        mainMenuPanel.SetActive(false);
        rightPanel.SetActive(false);
    }

    public void OnBattleEventClick()
    {
        mainMenuPanel.SetActive(true);
        rightPanel.SetActive(true);
        themePanel.SetActive(false);
    }

    public void OnPlayEventClick()
    {
        if(mainMenuPanelScript.hasBeenClicked && GameController.Instance.currentGameState == GameController.GameState.MainMenu)
        {
            GameController.Instance.onStartGame.Invoke();
            Instantiate(GameController.Instance.cannonPrefab, mainMenuPanelScript.cannonAnim.transform.position, Quaternion.identity);
            mainMenuPanelScript.hasBeenClicked = false;
            bottomPanel.SetActive(false);
        }
    }
}
