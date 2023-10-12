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

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public List<ItemData> items = new List<ItemData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            PlayerPrefs.SetInt("PointsInOneGame", 0);
        }
    }
    private void Start()
    {
        
    }

}
