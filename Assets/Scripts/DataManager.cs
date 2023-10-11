using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string name;
    public Sprite sprite;
    public float chance;
    public int cost;
}

[System.Serializable]
public class ThemeData
{
    public string name;
    public Sprite active;
    public bool pass;
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public List<ItemData> items = new List<ItemData>();
    public List<ThemeData> themes = new List<ThemeData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
