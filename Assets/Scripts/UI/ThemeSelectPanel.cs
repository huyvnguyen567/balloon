using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeSelectPanel : MonoBehaviour
{
    //[SerializeField] private int themeCount = 20;
    [SerializeField] private GameObject themePrefab;
    [SerializeField] private GameObject content;
    [SerializeField] private Sprite inActive;
    private GameObject theme;
    void Start()
    {
        for (int i = 0; i < DataManager.Instance.themes.Count; i++)
        {
            theme = Instantiate(themePrefab, content.transform, false);
            if (DataManager.Instance.themes[i].pass)
            {
                theme.GetComponent<Image>().sprite = DataManager.Instance.themes[i].active;
            }
            else
            {
                theme.GetComponent<Image>().sprite = inActive;
            }
        }
        
    }

    void Update()
    {
        
    }
}
