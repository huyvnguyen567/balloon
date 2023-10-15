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

    public int diamond = 1700;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            PlayerPrefs.SetInt(TaskType.PointsInOneGame.ToString(), 0);
            PlayerPrefs.SetInt(TaskType.DaysConsecutive.ToString(), 3);
        }
    }
    private void Start()
    {
        
    }

}
