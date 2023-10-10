using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject bigMainMenuPanel;

    [Header("Prefab")]
    public GameObject bigMainMenuPanelPrefab;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    public GameObject Spawn(GameObject ui)
    {
        return Instantiate(ui, transform, false);
    }
}
