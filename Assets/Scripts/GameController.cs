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

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private int currentLevel;
    [HideInInspector] public float ScreenWidth;

    public List<ItemData> items = new List<ItemData>();

    private void Awake()
    {
        Instance = this;
        ScreenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        Time.timeScale = 0.5f;
    }

    public ItemData GetRandomItem()
    {
        float totalChance = 0f;
        float randomValue = Random.Range(0f, 1f);

        foreach (var itemData in items)
        {
            totalChance += itemData.chance;
            if (randomValue <= totalChance)
            {
                return itemData;
            }
        }

        return null;
    }
    public int CurrentLevel
    {
        get { return currentLevel; }
        set { currentLevel = value; }
    }

}
