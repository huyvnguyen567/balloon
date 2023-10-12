using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject bigMainMenuPanel;
    public GameObject processTaskPopup;

    [Header("Parent Transform")]
    [SerializeField] private GameObject parentWindow;
    [SerializeField] private GameObject parentPopup;

    [Header("Prefab")]
    public GameObject bigMainMenuPanelPrefab;
    public GameObject processTaskPopupPrefab;

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
