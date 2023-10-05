using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private int currentLevel;
    [HideInInspector] public float ScreenWidth;
    [System.Serializable]
    public class ItemChance
    {
        public GameObject item;
        public float chance;
        public int cost;
    }

    public List<ItemChance> items = new List<ItemChance>();

    private void Awake()
    {
        Instance = this;
        ScreenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
    }

    public GameObject GetRandomItem()
    {
        float totalChance = 0f;
        float randomValue = Random.Range(0f, 1f);

        foreach (var itemChance in items)
        {
            totalChance += itemChance.chance;
            if (randomValue <= totalChance)
            {
                return itemChance.item;
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
