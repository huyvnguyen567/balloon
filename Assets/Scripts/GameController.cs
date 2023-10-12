using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public UnityEvent onStartGame = new UnityEvent(); // Sự kiện bấm chuột

    public enum GameState
    {
        MainMenu,
        Gameplay,
        Pause,
        Win,
        Lose
    }

    [SerializeField] private int currentLevel;
    public GameState currentGameState;
    [HideInInspector] public float ScreenWidth;
    public SpriteRenderer background;


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
  
  
    public void SwitchGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.MainMenu:
                UIManager.Instance.bigMainMenuPanel = UIManager.Instance.Spawn(UIType.Window, UIManager.Instance.bigMainMenuPanelPrefab);
                UIManager.Instance.processTaskPopup = UIManager.Instance.Spawn(UIType.Popup, UIManager.Instance.processTaskPopupPrefab);
                UIManager.Instance.processTaskPopup.SetActive(false);
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

        foreach (var itemData in DataManager.Instance.items)
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
