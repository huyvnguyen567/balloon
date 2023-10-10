using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public UnityEvent onStartGame = new UnityEvent(); // Sự kiện bấm chuột

    public enum GameState
    {
        MainMenu,
        Gameplay,
        ThemeSelect,
        Pause,
        Win,
        Lose
    }

    [SerializeField] private int currentLevel;
    public GameState currentGameState;
    [HideInInspector] public float ScreenWidth;

    public List<ItemData> items = new List<ItemData>();
    public GameObject cannonPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        ScreenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
    }
    private void Start()
    {
        SwitchGameState(GameState.MainMenu);
    }
    private void Update()
    {
        MainMenuPanel mainMenuPanel = UIManager.Instance.bigMainMenuPanel.GetComponent<BigMainMenuPanel>().mainMenuPanelScript;
        if (mainMenuPanel != null && currentGameState == GameState.MainMenu && mainMenuPanel.hasBeenClicked)
        {
            onStartGame.Invoke();
            mainMenuPanel.hasBeenClicked = false;
            Instantiate(cannonPrefab, mainMenuPanel.cannonAnim.transform.position, Quaternion.identity);
        }
    }
  
    public void SwitchGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.MainMenu:
                UIManager.Instance.bigMainMenuPanel = UIManager.Instance.Spawn(UIManager.Instance.bigMainMenuPanelPrefab);
                break;

            case GameState.ThemeSelect:  
                break;

            case GameState.Gameplay:
                // Xử lý khi chuyển sang Gameplay
                break;
            case GameState.Pause:
                // Xử lý khi dừng game
                break;
            case GameState.Lose:
                // Xử lý khi chuyển sang Lose
                break;
            case GameState.Win:
                // Xử lý khi chuyển sang Win
                break;
        }
        currentGameState = newState;
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
