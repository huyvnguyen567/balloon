using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [HideInInspector]
    public GameObject bigMainMenuPanel;
    public GameObject gamePlayWindow;
    public GameObject processTaskPopup;
    public GameObject cannonBuyPopup;
    public GameObject levelupPopup;
    public GameObject losePopup;
    public GameObject menuContentPopup;
    public GameObject textPopup;
    public GameObject comingSoonPopup;

    [Header("Parent Transform")]
    [SerializeField] private GameObject parentWindow;
    [SerializeField] private GameObject parentPopup;

    [Header("Prefab")]
    public GameObject bigMainMenuPanelPrefab;
    public GameObject gamePlayWindowPrefab;
    public GameObject processTaskPopupPrefab;
    public GameObject cannonBuyPopupPrefab;
    public GameObject levelupPopupPrefab;
    public GameObject losePopupPrefab;
    public GameObject menuContentPopupPrefab;
    public GameObject textPopupPrefab;
    public GameObject comingSoonPopupPrefab;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public GameObject Spawn(UIType type, GameObject ui)
    {
        switch (type)
        {
            case UIType.Window:
                return Instantiate(ui, parentWindow.transform, false);
            case UIType.Popup:
                return Instantiate(ui, parentPopup.transform, false);
            default:
                return null;
        }
    }

}
public enum UIType
{
    Window,
    Popup
}
