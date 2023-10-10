using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeSelectPanel : BaseUI
{
    [SerializeField] private int themeCount = 20;
    [SerializeField] private GameObject themePrefab;
    [SerializeField] private GameObject content;
    void Start()
    {
        for (int i = 0; i < themeCount; i++)
        {
            Instantiate(themePrefab, content.transform, false);
        }
    }

    void Update()
    {

    }
}
