using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public UnityEvent onStartGame = new UnityEvent(); // Sự kiện bấm chuột
    public static UnityEvent OnBuyClick = new UnityEvent();

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

    public int ballSize1DestroyedCount;
    public float targetProcess;
   

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
        DataManager.Instance.LoadLevel();
        DataManager.Instance.LoadScore();
        DataManager.Instance.LoadHighScore();
        StartLevel(currentLevel);
        SwitchGameState(GameState.MainMenu);
    }
  
  
    public void SwitchGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.MainMenu:
                UIManager.Instance.bigMainMenuPanel = UIManager.Instance.Spawn(UIType.Window, UIManager.Instance.bigMainMenuPanelPrefab);
                UIManager.Instance.processTaskPopup = UIManager.Instance.Spawn(UIType.Popup, UIManager.Instance.processTaskPopupPrefab);
                UIManager.Instance.cannonBuyPopup = UIManager.Instance.Spawn(UIType.Popup, UIManager.Instance.cannonBuyPopupPrefab);
                UIManager.Instance.processTaskPopup.SetActive(false);
                UIManager.Instance.cannonBuyPopup.SetActive(false);
                break;

            case GameState.Gameplay:
                UIManager.Instance.gamePlayWindow = UIManager.Instance.Spawn(UIType.Window, UIManager.Instance.gamePlayWindowPrefab);
                UpdateProcessGame();

                break;
            case GameState.Pause:
                // Xử lý khi dừng game
                break;
            case GameState.Lose:
                if (UIManager.Instance.losePopup == null)
                {
                    UIManager.Instance.losePopup = UIManager.Instance.Spawn(UIType.Popup, UIManager.Instance.losePopupPrefab);
                }

                break;
            case GameState.Win:
                UIManager.Instance.levelupPopup = UIManager.Instance.Spawn(UIType.Popup, UIManager.Instance.levelupPopupPrefab);
                break;
        }
        currentGameState = newState;
    }

    public void StartLevel(int level)
    {
        LevelSO.Level levelData = DataManager.Instance.levelData.levels[level];

        currentLevel = levelData.level;
        BallSpawner.Instance.ballsCount = levelData.numberOfMeteor;
        BallSpawner.Instance.minHealth = levelData.minHealth;
        BallSpawner.Instance.maxHealth = levelData.maxHealth;
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

    public void UpdateProcessGame()
    {
        foreach(GameObject ball in BallSpawner.Instance.balls)
        {
            targetProcess += Mathf.Pow(2, ball.GetComponent<BallFissionable>().size - 1);
        }
    }

    public void BuyCannon(int index)
    {
        CannonChangePanel cannonChangPanel = UIManager.Instance.bigMainMenuPanel.GetComponent<BigMainMenuPanel>().cannonPanel.GetComponent<CannonChangePanel>();
        List<CanonButtonSO> cannons = cannonChangPanel.cannons;

        CanonButtonSO cannonData = cannons[index];
        if (!cannonData.isPurchased && DataManager.Instance.diamond >= cannonData.price)
        {
            cannonData.isPurchased = true;
            DataManager.Instance.diamond -= cannonData.price;
            OnBuyClick.Invoke();
        }
        else
        {
            Debug.Log("Không đủ tiền");
        }
    }
    public int CurrentLevel
    {
        get { return currentLevel; }
        set { currentLevel = value; }
    }

}
